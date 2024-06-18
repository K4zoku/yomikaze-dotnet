using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Yomikaze.Application.Helpers;
using Yomikaze.Domain.Abstracts;
using Yomikaze.Domain.Models;

namespace Yomikaze.API.Main.Base;

public abstract class CrudControllerBase<T, TKey, TInput, TOutput>(
    DbContext dbContext,
    IMapper mapper,
    IRepository<T, TKey> repository)
    : ControllerBase
    where T : class, IEntity<TKey>
    where TInput : class
    where TOutput : class
{
    protected DbContext DbContext { get; set; } = dbContext;
    protected IMapper Mapper { get; set; } = mapper;

    protected IRepository<T, TKey> Repository { get; set; } = repository;


    [HttpGet("{key}")]
    public virtual ActionResult<TOutput> Get(TKey key)
    {
        T? entity = Repository.Get(key);

        if (entity == null)
        {
            throw new HttpResponseException(HttpStatusCode.NotFound, ResponseModel.CreateError("Not found"));
        }

        return Ok(Mapper.Map<TOutput>(entity));
    }

    [HttpPost]
    public virtual ActionResult<TOutput> Post(TInput input)
    {
        CheckModelState();

        T? entity = Mapper.Map<T>(input);
        Repository.Add(entity);
        return Ok(Mapper.Map<TOutput>(entity));
    }

    [HttpPut("{key}")]
    public virtual ActionResult<TOutput> Put(TKey key, TInput input)
    {
        CheckModelState();

        T? entityToUpdate = Repository.Get(key);
        if (entityToUpdate == null)
        {
            throw new HttpResponseException(HttpStatusCode.NotFound, ResponseModel.CreateError("Not found"));
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
            throw new HttpResponseException(HttpStatusCode.NotFound, ResponseModel.CreateError("Not found"));
        }

        Repository.Delete(entity);
        return Ok();
    }


    [NonAction]
    // check model state and throw exception if invalid
    protected void CheckModelState()
    {
        if (!ModelState.IsValid)
        {
            throw new HttpResponseException(HttpStatusCode.BadRequest, ResponseModel.CreateError("Invalid input"));
        }
    }
}

public abstract class CrudControllerBase<T, TInput, TOutput>(
    DbContext dbContext,
    IMapper mapper,
    IRepository<T> repository)
    : CrudControllerBase<T, ulong, TInput, TOutput>(dbContext, mapper, repository)
    where T : class, IEntity
    where TInput : class
    where TOutput : class;