﻿using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Yomikaze.Application.Helpers;

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
    protected IMapper Mapper { get; set; } = mapper;

    protected virtual TRepository Repository { get; set; } = repository;

    protected IDistributedCache Cache { get; set; } = cache;

    protected ILogger<CrudControllerBase<T, TKey, TModel, TRepository>> Logger { get; set; } = logger;

    protected PagedList<TModel> GetPaged(IQueryable<T> query, PaginationModel pagination)
    {
        int skip = (pagination.Page - 1) * pagination.Size;
        query = query.Skip(skip).Take(pagination.Size);
        return new PagedList<TModel>
        {
            CurrentPage = pagination.Page,
            PageSize = pagination.Size,
            RowCount = query.Count(),
            PageCount = (int)Math.Ceiling((double)query.Count() / pagination.Size),
            Results = Mapper.Map<TModel[]>(query.AsEnumerable())
        };
    }

    [HttpGet]
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
        Logger.LogDebug("Cache miss for {key}, storing data in cache...", keyName);
        Cache.SetInBackground(keyName, model);
        return Ok(model);
    }

    [HttpPost]
    public virtual ActionResult<TModel> Post(TModel input)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        T? entity = Mapper.Map<T>(input);
        Logger.LogInformation("After mapped {id}", entity.Id);
        Repository.Add(entity);
        Logger.LogInformation("After added {id}", entity.Id);
        Cache.Remove($"{KeyPrefix}:list*");
        entity = Repository.Get(entity.Id); // load navigation properties
        string id = entity?.Id?.ToString() ?? "-1"; 
        string url = Url.Action("Get", new { key = id }) ?? string.Empty; 
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
        catch (DbUpdateException)
        {
            return Conflict();
        }

        // remove cache
        string keyName = KeyPrefix + key;
        Cache.Remove(keyName);
        Cache.Remove($"{KeyPrefix}:list*");

        return NoContent();
    }

    [HttpPatch("{key}")]
    public virtual ActionResult<TModel> Patch(TKey key, JsonPatchDocument<TModel> patch)
    {
        T? entityToUpdate = Repository.Get(key);
        if (entityToUpdate == null)
        {
            return NotFound();
        }

        TModel model = Mapper.Map<TModel>(entityToUpdate);

        Logger.LogInformation("Patching model: {model}", JsonConvert.SerializeObject(model));
        patch.ApplyTo(model);
        Logger.LogInformation("Patched model: {model}", JsonConvert.SerializeObject(model));

        Mapper.Map(model, entityToUpdate);
        try
        {
            Repository.Update(entityToUpdate);
        }
        catch (DbUpdateException e)
        {
            Logger.LogTrace(e, "Error updating entity {key}", key);
            return Conflict();
        }

        // remove cache
        string keyName = KeyPrefix + key;
        Cache.Remove(keyName);
        Cache.Remove($"{KeyPrefix}:list*");

        return NoContent();
    }

    [HttpDelete("{key}")]
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
        options ??= new DistributedCacheEntryOptions() { SlidingExpiration = TimeSpan.FromMinutes(5) };
        Task.Run(async () =>
        {
            await cache.SetAsync(key, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)), options);
        });
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
    where TRepository : IRepository<T>;