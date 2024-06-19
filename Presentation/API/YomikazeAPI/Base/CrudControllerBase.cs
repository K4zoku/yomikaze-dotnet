using Microsoft.AspNetCore.JsonPatch;
using System.Net;
using System.Text.Json;
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


    [HttpGet]
    public virtual async Task<ActionResult<ICollection<TModel>>> List(int? page, int? pageSize)
    {
        int actualPage = page ?? 0;
        int actualPageSize = pageSize ?? 10;
        string keyName = $"{KeyPrefix}:list({page},{pageSize})";
        if (Cache.TryGet(keyName, out ICollection<TModel>? cachedModels))
        {
            Logger.LogDebug("Cache hit for {key}, returning cached data...", keyName);
            return Ok(cachedModels);
        }

        T[] entities = await Repository.Query().Skip(actualPage * actualPageSize).Take(actualPageSize).ToArrayAsync();
        TModel[] models = Mapper.Map<TModel[]>(entities);
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
        Repository.Add(entity);
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
        patch.ApplyTo(model);
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

        TC? cachedValue = JsonSerializer.Deserialize<TC>(cachedData);
        value = cachedValue;
        return cachedValue != null;
    }

    internal static void SetInBackground<TC>(this IDistributedCache cache, string key, TC value,
        DistributedCacheEntryOptions options)
    {
        Task.Run(async () => await cache.SetAsync(key, JsonSerializer.SerializeToUtf8Bytes(value), options));
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