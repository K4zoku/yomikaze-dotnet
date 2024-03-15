using Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace Yomikaze.API.OData.Base;

[EnableQuery]
public abstract class ODataControllerBase<T, TKey>(DbContext dbContext, IRepo<T, TKey> repository) : ControllerBase
    where T : class, IEntity<TKey>
{
    protected DbContext DbContext { get; set; } = dbContext;

    protected IRepo<T, TKey> Repository { get; set; } = repository;


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

public abstract class ODataControllerBase<T>(DbContext dbContext, IRepo<T, long> repository)
    : ODataControllerBase<T, long>(dbContext, repository)
    where T : class, IEntity<long>;