using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yomikaze.Domain.Common;

namespace Yomikaze.API.Main.Base;

public abstract class CrudControllerBase<T, TKey, TInput, TOutput>(
    DbContext dbContext,
    IMapper mapper,
    IRepo<T, TKey> repository)
    : ControllerBase
    where T : class, IEntity<TKey>
    where TInput : class
    where TOutput : class
{
    protected DbContext DbContext { get; set; } = dbContext;
    protected IMapper Mapper { get; set; } = mapper;

    protected IRepo<T, TKey> Repository { get; set; } = repository;

    [HttpGet]
    public virtual ActionResult<IEnumerable<TOutput>> Get()
    {
        return Ok(Mapper.Map<IEnumerable<TOutput>>(Repository.Query()));
    }


    [HttpGet("{key}")]
    public virtual ActionResult<TOutput> Get(TKey key)
    {
        T? entity = Repository.Get(key);
        if (entity == null)
        {
            return NotFound();
        }

        return Ok(Mapper.Map<TOutput>(entity));
    }

    [HttpPost]
    public virtual ActionResult<TOutput> Post(TInput input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        T? entity = Mapper.Map<T>(input);
        Repository.Add(entity);
        return Ok(Mapper.Map<TOutput>(entity));
    }

    [HttpPut("{key}")]
    public virtual ActionResult<TOutput> Put(TKey key, TInput input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        T? entityToUpdate = Repository.Get(key);
        if (entityToUpdate == null)
        {
            return NotFound();
        }

        Mapper.Map(input, entityToUpdate);
        Repository.Update(entityToUpdate);
        return Ok(Mapper.Map<TOutput>(entityToUpdate));
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
        return Ok();
    }
}

public abstract class CrudControllerBase<T, TInput, TOutput>(
    DbContext dbContext,
    IMapper mapper,
    IRepo<T, long> repository)
    : CrudControllerBase<T, long, TInput, TOutput>(dbContext, mapper, repository)
    where T : class, IEntity<long>
    where TInput : class
    where TOutput : class;