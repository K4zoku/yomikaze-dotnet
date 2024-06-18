using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Yomikaze.API.Main.Base;
using Yomikaze.Application.Data.Repos;
using Yomikaze.Application.Helpers;
using Yomikaze.Domain.Entities;
using Yomikaze.Domain.Models;

namespace Yomikaze.API.Main.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Administrator")]
public class TagsController(DbContext dbContext, IMapper mapper)
    : CrudControllerBase<Tag, GenreInputModel, GenreOutputModel>(dbContext, mapper, new GenreRepository(dbContext))
{
    [HttpPost]
    public override ActionResult<GenreOutputModel> Post(GenreInputModel input)
    {
        CheckModelState();

        Tag? entity = Mapper.Map<Tag>(input);
        if (Repository.Query().Any(x => x.Name == entity.Name))
        {
            throw new HttpResponseException(HttpStatusCode.Conflict, ResponseModel.CreateError("Genre already exists"));
        }
        Repository.Add(entity);
        return Ok(Mapper.Map<GenreOutputModel>(entity));
    }
    
    [HttpPut("{key}")]
    public override ActionResult<GenreOutputModel> Put(ulong key, GenreInputModel input)
    {
        CheckModelState();

        Tag? entityToUpdate = Repository.Get(key);
        if (entityToUpdate == null)
        {
            throw new HttpResponseException(HttpStatusCode.NotFound, ResponseModel.CreateError("Not found"));
        }

        Mapper.Map(input, entityToUpdate);
        if (Repository.Query().Any(x => x.Name == entityToUpdate.Name && x.Id != entityToUpdate.Id))
        {
            throw new HttpResponseException(HttpStatusCode.Conflict, ResponseModel.CreateError("Genre already exists"));
        }
        Repository.Update(entityToUpdate);
        return Ok(Mapper.Map<GenreOutputModel>(entityToUpdate));
    }
}