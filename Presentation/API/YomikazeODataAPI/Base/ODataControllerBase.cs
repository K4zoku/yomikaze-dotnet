using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Yomikaze.Domain.Abstracts;

namespace Yomikaze.API.OData.Base;

[EnableQuery]
public abstract class ODataControllerBase<T, TKey>(IRepository<T, TKey> repository) : ODataController
    where T : class, IEntity<TKey>
{
    private IRepository<T, TKey> Repository { get; } = repository;


    public ActionResult<IEnumerable<T>> Get()
    {
        return Ok(Repository.Query());
    }


    public IActionResult Get([FromRoute] TKey key)
    {
        T? entity = Repository.Get(key);
        if (entity == null)
        {
            return NotFound();
        }

        return Ok(entity);
    }

    // public IActionResult Post(T entity)
    // {
    //     if (!ModelState.IsValid)
    //     {
    //         return BadRequest(ModelState);
    //     }
    //     Repository.Add(entity);
    //     return Created(entity);
    // }
    //
    // public IActionResult Put([FromRoute] TKey key, T entity)
    // {
    //     if (!ModelState.IsValid)
    //     {
    //         return BadRequest(ModelState);
    //     }
    //     T? existing = Repository.Get(key);
    //     if (existing == null)
    //     {
    //         return NotFound();
    //     }
    //     Repository.Update(entity);
    //     return Updated(entity);
    // }


    // public IActionResult Patch([FromRoute] TKey key, Delta<T> delta)
    // {
    //     if (!ModelState.IsValid)
    //     {
    //         return BadRequest(ModelState);
    //     }
    //     T? existing = Repository.Get(key);
    //     if (existing == null)
    //     {
    //         return NotFound();
    //     }
    //     delta.Patch(existing);
    //     Repository.Update(existing);
    //     return Updated(existing);
    // }

    // public IActionResult Delete([FromRoute] TKey key)
    // {
    //     T? existing = Repository.Get(key);
    //     if (existing == null)
    //     {
    //         return NotFound();
    //     }
    //     Repository.Delete(existing);
    //     return Ok();
    // }
}

public abstract class ODataControllerBase<T>(IRepository<T, ulong> repository)
    : ODataControllerBase<T, ulong>(repository)
    where T : class, IEntity;