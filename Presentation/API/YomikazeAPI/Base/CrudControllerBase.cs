using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Text;
using Yomikaze.Application.Helpers;

namespace Yomikaze.API.Main.Base;

public abstract class CrudControllerBase<T, TKey, TModel>(
    DbContext dbContext,
    IMapper mapper,
    IRepository<T, TKey> repository,
    IDistributedCache cache,
    ILogger<CrudControllerBase<T, TKey, TModel>> logger)
    : ControllerBase
    where T : class, IEntity<TKey>
    where TModel : class
{
    private static readonly string KeyPrefix = typeof(T).Name + ":";
    protected DbContext DbContext { get; set; } = dbContext;
    protected IMapper Mapper { get; set; } = mapper;

    protected IRepository<T, TKey> Repository { get; set; } = repository;

    protected IDistributedCache Cache { get; set; } = cache;

    protected ILogger<CrudControllerBase<T, TKey, TModel>> Logger { get; set; } = logger;

    public class PagedResult
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }
        public int PageCount { get; set; }
        public IEnumerable<TModel> Results { get; set; } = [];
    }

    protected PagedResult GetPaged(IQueryable<T> query, PaginationModel pagination)
    {
        int skip = (pagination.Page - 1) * pagination.Size;
        return new PagedResult
        {
            CurrentPage = pagination.Page, 
            PageSize = pagination.Size, 
            RowCount = query.Count(),
            PageCount = (int)Math.Ceiling((double) query.Count() / pagination.Size),
            Results = Mapper.Map<TModel[]>(query.Skip(skip).Take(pagination.Size))
        };
    }

    [HttpGet]
    public virtual ActionResult<PagedResult> List([FromQuery] PaginationModel pagination)
    {
        string keyName = $"{KeyPrefix}:list({pagination.Page}, {pagination.Size})";
        if (Cache.TryGet(keyName, out PagedResult? cachedModels))
        {
            Logger.LogDebug("Cache hit for {key}, returning cached data...", keyName);
            return Ok(cachedModels);
        }

        PagedResult models = GetPaged(Repository.Query(), pagination);
        Logger.LogDebug("Cache miss for {key}, storing data in cache...", keyName);
        Cache.SetInBackground(keyName, models,
            new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) });
        Logger.LogDebug("Returning {key} data...", keyName);
        return models;
    }

    [HttpGet("{key}")]
    public virtual ActionResult<TModel> Get(TKey key)
    {
        string keyName = KeyPrefix + key;

        if (Cache.TryGet(keyName, out TModel? cachedModel))
        {
            Logger.LogDebug("Cache hit for {key}, returning cached data...", keyName);
            return Ok(cachedModel);
        }

        T? entity = Repository.Get(key);

        if (entity == null)
        {
            throw new HttpResponseException(HttpStatusCode.NotFound, "Not found");
        }

        TModel model = Mapper.Map<TModel>(entity);
        Logger.LogDebug("Cache miss for {key}, storing data in cache...", keyName);
        Cache.SetInBackground(keyName, model,
            new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) });
        return Ok(model);
    }

    [HttpPost]
    public virtual ActionResult<TModel> Post(TModel input)
    {
        if (!ModelState.IsValid)
        {
            throw new HttpResponseException(HttpStatusCode.BadRequest, "Validation failed");
        }

        T? entity = Mapper.Map<T>(input);
        Logger.LogInformation("After mapped {id}", entity.Id);
        Repository.Add(entity);
        Logger.LogInformation("After added {id}", entity.Id);
        Cache.Remove($"{KeyPrefix}:list*");
        
        return Ok(Mapper.Map<TModel>(entity));
    }

    [HttpPut("{key}")]
    public virtual ActionResult<TModel> Put(TKey key, TModel input)
    {
        if (!ModelState.IsValid)
        {
            throw new HttpResponseException(HttpStatusCode.BadRequest, "Validation failed");
        }

        T? entityToUpdate = Repository.Get(key);
        if (entityToUpdate == null)
        {
            throw new HttpResponseException(HttpStatusCode.NotFound, "Not found");
        }

        Mapper.Map(input, entityToUpdate);

        try
        {
            Repository.Update(entityToUpdate);
        }
        catch (DbUpdateException)
        {
            throw new HttpResponseException(HttpStatusCode.Conflict, "Entity already exists");
        }

        // remove cache
        string keyName = KeyPrefix + key;
        Cache.Remove(keyName);
        Cache.Remove($"{KeyPrefix}:list*");

        return Ok(Mapper.Map<TModel>(entityToUpdate));
    }

    [HttpPatch]
    public virtual ActionResult<TModel> Patch(TKey key, JsonPatchDocument<TModel> patch)
    {
        T? entityToUpdate = Repository.Get(key);
        if (entityToUpdate == null)
        {
            throw new HttpResponseException(HttpStatusCode.NotFound, "Not found");
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
        catch (DbUpdateException)
        {
            throw new HttpResponseException(HttpStatusCode.Conflict, "Entity already exists");
        }

        // remove cache
        string keyName = KeyPrefix + key;
        Cache.Remove(keyName);
        Cache.Remove($"{KeyPrefix}:list*");

        return Ok(Mapper.Map<TModel>(entityToUpdate));
    }

    [HttpDelete("{key}")]
    public virtual ActionResult Delete(TKey key)
    {
        T? entity = Repository.Get(key);

        if (entity == null)
        {
            throw new HttpResponseException(HttpStatusCode.NotFound, "Not found");
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
    internal static bool TryGet<TC>(this IDistributedCache cache, string key, out TC? value)
    {
        byte[]? cachedData = cache.Get(key);
        if (cachedData == null)
        {
            value = default;
            return false;
        }
        TC? cachedValue = JsonConvert.DeserializeObject<TC>(Encoding.UTF8.GetString(cachedData));
        value = cachedValue;
        return cachedValue != null;
    }

    internal static void SetInBackground<TC>(this IDistributedCache cache, string key, TC value,
        DistributedCacheEntryOptions options)
    {
        Task.Run(async () =>
        {
            await cache.SetAsync(key, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)), options);
        });
    }
}

public abstract class CrudControllerBase<T, TModel>(
    DbContext dbContext,
    IMapper mapper,
    IRepository<T> repository,
    IDistributedCache cache,
    ILogger<CrudControllerBase<T, TModel>> logger)
    : CrudControllerBase<T, ulong, TModel>(dbContext, mapper, repository, cache, logger)
    where T : class, IEntity
    where TModel : class;