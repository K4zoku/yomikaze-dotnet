using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Yomikaze.Domain.Abstracts;

namespace Yomikaze.API.OData.Base;

[EnableQuery]
public abstract class ODataControllerBase<T, TKey>(IRepository<T, TKey> repository) : ControllerBase
    where T : class, IEntity<TKey>
{
    protected IRepository<T, TKey> Repository { get; set; } = repository;


    public virtual ActionResult<IEnumerable<T>> Get()
    {
        return Ok(Repository.Query());
    }


    public virtual ActionResult<T> Get(TKey key)
    {
        T? entity = Repository.Get(key);
        if (entity == null)
        {
            return NotFound();
        }

        return Ok(entity);
    }
}

public abstract class ODataControllerBase<T>(IRepository<T> repository)
    : ODataControllerBase<T, string>(repository)
    where T : class, IEntity;