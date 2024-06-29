﻿using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Reflection;
using System.Text;
using Yomikaze.Domain;

namespace Yomikaze.API.Main.Base;

public abstract class CrudControllerBase<T, TKey, TModel, TRepository>(
    TRepository repository,
    IMapper mapper,
    IDistributedCache cache,
    ILogger<CrudControllerBase<T, TKey, TModel, TRepository>> logger)
    : ControllerBase
    where T : class, IEntity<TKey>
    where TModel : class
    where TRepository : IRepository<T, TKey>
{
    protected static readonly string KeyPrefix = typeof(T).Name + ":";
    protected IMapper Mapper { get; } = mapper;

    protected TRepository Repository { get; } = repository;

    protected IDistributedCache Cache { get; } = cache;

    protected ILogger<CrudControllerBase<T, TKey, TModel, TRepository>> Logger { get; } = logger;

    [NonAction]
    protected PagedList<TModel> GetPaged(IQueryable<T> query, PaginationModel pagination)
    {
        int skip = (pagination.Page - 1) * pagination.Size;
        query = query.Skip(skip).Take(pagination.Size);
        var results = Mapper.Map<List<TModel>>(query.AsEnumerable());
        results.ForEach(model => ModelWriteOnlyProperties.ForEach(property => property.SetValue(model, default)));
        return new PagedList<TModel>
        {
            CurrentPage = pagination.Page,
            PageSize = pagination.Size,
            RowCount = query.Count(),
            PageCount = (int)Math.Ceiling((double)query.Count() / pagination.Size),
            Results = results
        };
    }

    protected List<PropertyInfo> ModelWriteOnlyProperties { get; } = typeof(TModel).GetProperties()
        .Where(x => x.GetCustomAttribute<WriteOnlyAttribute>() != null)
        .ToList();

    [HttpGet]
    [SwaggerResponse((int)HttpStatusCode.OK, $"List of entity", ContentTypes = ["application/json"])]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal server error")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Unauthorized", ContentTypes = ["application/json"])]
    [SwaggerResponse((int)HttpStatusCode.Forbidden, "Forbidden", ContentTypes = ["application/json"])]
    public virtual ActionResult<PagedList<TModel>> List([FromQuery] PaginationModel pagination)
    {
        string keyName = $"{KeyPrefix}:list({pagination.Page}, {pagination.Size})";
        if (Cache.TryGet(keyName, out PagedList<TModel> cachedModels))
        {
            Logger.LogDebug("Cache hit for {key}, returning cached data...", keyName);
            return Ok(cachedModels);
        }

        PagedList<TModel> models = GetPaged(Repository.Query(), pagination);
        Logger.LogDebug("Cache miss for {key}, storing data in cache...", keyName);
        Cache.SetInBackground(keyName, models);
        Logger.LogDebug("Returning {key} data...", keyName);
        return models;
    }

    [HttpGet("{key}")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Entity found", ContentTypes = ["application/json"])]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal server error")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Unauthorized", ContentTypes = ["application/json"])]
    [SwaggerResponse((int)HttpStatusCode.Forbidden, "Forbidden", ContentTypes = ["application/json"])]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Not found", ContentTypes = ["application/json"])]
    public virtual ActionResult<TModel> Get(TKey key)
    {
        string keyName = KeyPrefix + key;

        if (Cache.TryGet(keyName, out TModel cachedModel))
        {
            Logger.LogDebug("Cache hit for {key}, returning cached data...", keyName);
            return Ok(cachedModel);
        }

        T? entity = Repository.Get(key);

        if (entity == null)
        {
            Logger.LogWarning("Entity with key {key} not found", key);
            return NotFound();
        }

        TModel model = Mapper.Map<TModel>(entity);
        ModelWriteOnlyProperties.ForEach(x => x.SetValue(model, default));
        Logger.LogDebug("Cache miss for {key}, storing data in cache...", keyName);
        Cache.SetInBackground(keyName, model);
        return Ok(model);
    }

    [HttpPost]
    [SwaggerResponse((int)HttpStatusCode.Created, "Entity created", ContentTypes = ["application/json"])]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Problem when creating entity", ContentTypes = ["application/json"])]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Unauthorized", ContentTypes = ["application/json"])]
    [SwaggerResponse((int)HttpStatusCode.Forbidden, "Forbidden", ContentTypes = ["application/json"])]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Not found", ContentTypes = ["application/json"])]
    [SwaggerResponse((int)HttpStatusCode.Conflict, "Entity may already exists or some relationship does not exists",
        ContentTypes = ["application/json"])]
    public virtual ActionResult<TModel> Post(TModel input)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        T? entity = Mapper.Map<T>(input);
        Logger.LogDebug("After mapped {entity}", JsonConvert.SerializeObject(entity));
        try
        {
            Repository.Add(entity);
        } catch (DbUpdateException e)
        {
            Logger.LogWarning(e, "Error when adding entity");
            return Conflict();
        } catch (Exception e)
        {
            Logger.LogCritical(e, "Critical error when adding entity");
            return Problem();
        }

        Logger.LogDebug("After added {entity}", JsonConvert.SerializeObject(entity));
        Cache.Remove($"{KeyPrefix}:list*");
        entity = Repository.Get(entity.Id); // load navigation properties
        string id = entity?.Id?.ToString() ?? "-1";
        string url = Url.Action("Get", new { key = id }) ?? string.Empty;
        var model = Mapper.Map<TModel>(entity);
        ModelWriteOnlyProperties.ForEach(x => x.SetValue(model, default));
        return Created(url, Mapper.Map<TModel>(entity));
    }

    [HttpPut("{key}")]
    public virtual ActionResult<TModel> Put(TKey key, TModel input)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        T? entityToUpdate = Repository.Get(key);
        if (entityToUpdate == null)
        {
            return NotFound();
        }

        Mapper.Map(input, entityToUpdate);

        try
        {
            Repository.Update(entityToUpdate);
        }
        catch (DbUpdateException e)
        {
            Logger.LogWarning(e, "DbRelation error when updating entity {key}", key);
            return Conflict();
        } catch (Exception e)
        {
            Logger.LogCritical(e, "Critical error when updating entity {key}", key);
            return Problem();
        }

        // remove cache
        string keyName = KeyPrefix + key;
        Cache.Remove(keyName);
        Cache.Remove($"{KeyPrefix}:list*");

        return NoContent();
    }

    [HttpPatch("{key}")]    
    [SwaggerResponse((int)HttpStatusCode.NoContent, "Entity updated", ContentTypes = ["application/json"])] 
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Problem when updating entity", ContentTypes = ["application/json"])]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Unauthorized", ContentTypes = ["application/json"])]
    [SwaggerResponse((int)HttpStatusCode.Forbidden, "Forbidden", ContentTypes = ["application/json"])]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Not found", ContentTypes = ["application/json"])]
    [SwaggerResponse((int)HttpStatusCode.Conflict, "Entity may already exists or some relationship does not exists",
        ContentTypes = ["application/json"])]
    public virtual ActionResult<TModel> Patch(TKey key, JsonPatchDocument<TModel> patch)
    {
        T? entityToUpdate = Repository.Get(key);
        if (entityToUpdate == null)
        {
            return NotFound();
        }

        TModel model = Mapper.Map<TModel>(entityToUpdate);

        Logger.LogDebug("Patching model: {model}", JsonConvert.SerializeObject(model));
        patch.ApplyTo(model);
        Logger.LogDebug("Patched model: {model}", JsonConvert.SerializeObject(model));

        Mapper.Map(model, entityToUpdate);
        try
        {
            Repository.Update(entityToUpdate);
        }
        catch (DbUpdateException e)
        {
            Logger.LogWarning(e, "DbRelation error when updating entity {key}", key);
            return Conflict();
        } catch (Exception e)
        {
            Logger.LogCritical(e, "Critical error when updating entity {key}", key);
            return Problem();
        }

        // remove cache
        string keyName = KeyPrefix + key;
        Cache.Remove(keyName);
        Cache.Remove($"{KeyPrefix}:list*");

        return NoContent();
    }

    [HttpDelete("{key}")]
    [SwaggerResponse((int)HttpStatusCode.NoContent, "Entity deleted", ContentTypes = ["application/json"])] 
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal server error")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Unauthorized", ContentTypes = ["application/json"])]
    [SwaggerResponse((int)HttpStatusCode.Forbidden, "Forbidden", ContentTypes = ["application/json"])]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Not found", ContentTypes = ["application/json"])]
    public virtual ActionResult Delete(TKey key)
    {
        T? entity = Repository.Get(key);

        if (entity == null)
        {
            return NotFound();
        }

        Repository.Delete(entity);

        // remove cache
        string keyName = KeyPrefix + key;
        Cache.Remove(keyName);
        Cache.Remove($"{KeyPrefix}:list*");

        return NoContent();
    }
}

internal static class DistributedCacheExtension
{
    internal static bool TryGet<TC>(this IDistributedCache cache, string key, out TC value)
    {
        byte[]? cachedData = cache.Get(key);
        value = default!;
        if (cachedData == null)
        {
            return false;
        }

        TC? cachedValue = JsonConvert.DeserializeObject<TC>(Encoding.UTF8.GetString(cachedData));
        if (cachedValue == null)
        {
            return false;
        }

        value = cachedValue;
        return true;
    }

    internal static void SetInBackground<TC>(this IDistributedCache cache, string key, TC value,
        DistributedCacheEntryOptions? options = default!)
    {
        if (cache is NoCache) { return; }
        options ??= new DistributedCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes(5) };
        Task.Run(async () =>
        {
            await cache.SetAsync(key, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)), options);
        });
    }
}

internal class NoCache : IDistributedCache
{
    private static NoCache? _instance;
    private static readonly object Lock = new();
    
    public static NoCache Instance
    {
        get
        {
            lock (Lock)
            {
                return _instance ??= new NoCache();
            }
        }
    }
    public byte[]? Get(string key)
    {
        return null;
    }

    public Task<byte[]?> GetAsync(string key, CancellationToken token = new CancellationToken())
    {
        return Task.FromResult<byte[]?>(null);
    }

    public void Refresh(string key)
    {
        // do nothing
    }

    public Task RefreshAsync(string key, CancellationToken token = new CancellationToken())
    {
        return Task.CompletedTask;
    }

    public void Remove(string key)
    {
        // do nothing
    }

    public Task RemoveAsync(string key, CancellationToken token = new CancellationToken())
    {
        return Task.CompletedTask;
    }

    public void Set(string key, byte[] value, DistributedCacheEntryOptions options)
    {
        // do nothing
    }

    public Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options,
        CancellationToken token = new CancellationToken())
    {
        return Task.CompletedTask;
    }
}

public abstract class CrudControllerBase<T, TModel, TRepository>(
    TRepository repository,
    IMapper mapper,
    IDistributedCache cache,
    ILogger<CrudControllerBase<T, TModel, TRepository>> logger)
    : CrudControllerBase<T, ulong, TModel, TRepository>(repository, mapper, cache, logger)
    where T : class, IEntity
    where TModel : class
    where TRepository : IRepository<T>
{
    protected CrudControllerBase(TRepository repository, IMapper mapper, ILogger<CrudControllerBase<T, TModel, TRepository>> logger)
        : this(repository, mapper, NoCache.Instance, logger)
    {
    }
}