using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using System.Reflection;
using Yomikaze.API.Main.Helpers;
using Yomikaze.Domain;

namespace Yomikaze.API.Main.Base;

#pragma warning disable CA1005
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
    protected static readonly string CacheKeyPrefix = typeof(T).Name + ":";
    protected IMapper Mapper { get; } = mapper;

    protected TRepository Repository { get; } = repository;

    protected IDistributedCache Cache { get; } = cache;

    protected ILogger<CrudControllerBase<T, TKey, TModel, TRepository>> Logger { get; } = logger;

    protected List<PropertyInfo> ModelWriteOnlyProperties { get; } = typeof(TModel).GetProperties()
        .Where(x => x.GetCustomAttribute<WriteOnlyAttribute>() != null)
        .ToList();

    [NonAction]
    protected virtual PagedList<TModel> GetPaged(IQueryable<T> query, PaginationModel pagination)
    {
        int skip = (pagination.Page - 1) * pagination.Size;
        long count = query.LongCount();
        query = query.Skip(skip).Take(pagination.Size);
        List<TModel>? results = Mapper.Map<List<TModel>>(query.ToList());
        results.ForEach(model => ModelWriteOnlyProperties.ForEach(property => property.SetValue(model, default)));
        return new PagedList<TModel>
        {
            CurrentPage = pagination.Page,
            PageSize = pagination.Size,
            Totals = count,
            TotalPages = (int)Math.Ceiling((double)count / pagination.Size),
            Results = results
        };
    }

    protected string GetCacheKey(TKey key)
    {
        return CacheKeyPrefix + key;
    }

    [HttpGet]
    public virtual ActionResult<PagedList<TModel>> List([FromQuery] PaginationModel pagination)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        string keyName = $"{CacheKeyPrefix}{nameof(List)}:[{pagination.Page}, {pagination.Size}]";
        PagedList<TModel> result = Cache.GetOrSet(keyName, () =>
        {
            IQueryable<T> query = ListQuery();
            PagedList<TModel> models = GetPaged(query, pagination);
            return models;
        }, logger: Logger);
        return Ok(result);
    }

    protected virtual IQueryable<T> ListQuery()
    {
        return Repository.Query();
    }

    protected virtual T? GetEntity(TKey key)
    {
        return Repository.Get(key);
    }

    [HttpGet("{key}")]
    public virtual ActionResult<TModel> Get(TKey key)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        TModel? result = Cache.GetOrSet(GetCacheKey(key), () =>
        {
            T? entity = GetEntity(key);
            if (entity == null)
            {
                return null;
            }

            TModel model = Mapper.Map<TModel>(entity);
            ModelWriteOnlyProperties.ForEach(x => x.SetValue(model, default));
            return model;
        }, logger: Logger);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public virtual ActionResult<TModel> Post(TModel input)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        T entity = Mapper.Map<T>(input);
        try
        {
            Repository.Add(entity);
        }
        catch (DbUpdateException e)
        {
            Logger.LogWarning(e, "Error when adding entity");
            return Conflict();
        }
        catch (Exception e)
        {
            Logger.LogCritical(e, "Critical error when adding entity");
            return Problem();
        }

        RemoveListCache();
        if (Equals(entity.Id, default(TKey)))
        {
            return Problem("Id is null");
        }

        entity = Repository.Get(entity.Id) ?? entity;
        TModel? model = Mapper.Map<TModel>(entity);
        ModelWriteOnlyProperties.ForEach(x => x.SetValue(model, default));
        return CreatedAtAction("Get", new { key = entity.Id }, model);
    }

    [HttpPatch("{key}")]
    public virtual ActionResult<TModel> Patch(TKey key, JsonPatchDocument<TModel> patch)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        T? entityToUpdate = GetEntity(key);
        if (entityToUpdate == null)
        {
            return NotFound();
        }

        TModel model = Mapper.Map<TModel>(entityToUpdate);

        Logger.LogDebug("Patching model: {Model}", JsonConvert.SerializeObject(model));
        patch.ApplyTo(model);
        Logger.LogDebug("Patched model: {Model}", JsonConvert.SerializeObject(model));

        Mapper.Map(model, entityToUpdate);
        try
        {
            Repository.Update(entityToUpdate);
        }
        catch (DbUpdateException e)
        {
            Logger.LogWarning(e, "DbRelation error when updating entity {Key}", key);
            return Conflict();
        }
        catch (Exception e)
        {
            Logger.LogCritical(e, "Critical error when updating entity {Key}", key);
            return Problem();
        }

        RemoveCache(key);
        return NoContent();
    }

    [HttpDelete("{key}")]
    public virtual ActionResult Delete(TKey key)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        T? entity = GetEntity(key);
        if (entity == null)
        {
            return NotFound();
        }

        Repository.Delete(entity);
        RemoveCache(key);
        return NoContent();
    }

    protected virtual void RemoveCache(TKey key)
    {
        Cache.Remove(GetCacheKey(key));
        RemoveListCache();
    }

    protected virtual void RemoveListCache()
    {
        Cache.Remove($"{CacheKeyPrefix}{nameof(List)}:*");
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
    protected CrudControllerBase(TRepository repository, IMapper mapper,
        ILogger<CrudControllerBase<T, TModel, TRepository>> logger)
        : this(repository, mapper, NoCache.Instance, logger)
    {
    }
}

public abstract class SearchControllerBase<T, TModel, TRepository, TSearch>(
    TRepository repository,
    IMapper mapper,
    IDistributedCache cache,
    ILogger<SearchControllerBase<T, TModel, TRepository, TSearch>> logger)
    : CrudControllerBase<T, TModel, TRepository>(repository, mapper, cache, logger)
    where T : class, IEntity
    where TModel : class
    where TRepository : IRepository<T>
    where TSearch : class
{
    protected SearchControllerBase(TRepository repository, IMapper mapper,
        ILogger<SearchControllerBase<T, TModel, TRepository, TSearch>> logger)
        : this(repository, mapper, NoCache.Instance, logger)
    {
    }

    protected abstract IList<SearchFieldMutator<T, TSearch>> SearchFieldMutators { get; }

    protected IQueryable<T> ApplySearch(IQueryable<T> query, TSearch search)
    {
        return SearchFieldMutators.Aggregate(query, (current, mutator) => mutator.Apply(search, current));
    }

    [NonAction]
    public override ActionResult<PagedList<TModel>> List(PaginationModel pagination)
    {
        throw new NotSupportedException($"Use {nameof(List)} with search parameter");
    }

    [HttpGet]
    public virtual ActionResult<PagedList<TModel>> List([FromQuery] TSearch search,
        [FromQuery] PaginationModel pagination)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        string keyName =
            $"{CacheKeyPrefix}{nameof(List)}:{JsonConvert.SerializeObject(search)}:[{pagination.Page},{pagination.Size}]";

        return Cache.GetOrSet(keyName, () =>
        {
            IQueryable<T> query = ListQuery();
            query = ApplySearch(query, search);
            return GetPaged(query, pagination);
        }, logger: Logger);
    }
}

#pragma warning restore CA1005