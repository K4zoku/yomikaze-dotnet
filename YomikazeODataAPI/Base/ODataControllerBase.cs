using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Yomikaze.Domain.Common;

namespace Yomikaze.API.OData.Base;

[EnableQuery]
public abstract class ODataControllerBase<T, TKey, TInput, TOutput> : ControllerBase where T : class, IEntity<TKey>
{
    protected DbContext DbContext { get; set; }

    protected IRepo<T, TKey> Repository { get; set; }

    protected ODataControllerBase(DbContext dbContext, IRepo<T, TKey> repository)
    {
        DbContext = dbContext;
        Repository = repository;
    }


    public virtual ActionResult<IEnumerable<T>> Get()
    {
        return Ok(Repository.Query());
    }



    public virtual ActionResult<T> Get(TKey key)
    {
        var entity = Repository.Get(key);
        if (entity == null) return NotFound();
        return Ok(entity);
    }

}

public abstract class ODataControllerBase<T> : ODataControllerBase<T, long, T, T> where T : class, IEntity<long>
{
    protected ODataControllerBase(DbContext dbContext, IRepo<T, long> repository) : base(dbContext, repository)
    {
    }
}