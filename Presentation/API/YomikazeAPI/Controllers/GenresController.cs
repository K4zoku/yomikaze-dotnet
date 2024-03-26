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
public class GenresController(DbContext dbContext, IMapper mapper)
    : CrudControllerBase<Genre, GenreInputModel, GenreOutputModel>(dbContext, mapper, new GenreRepository(dbContext))
{
    [HttpPost]
    public override ActionResult<GenreOutputModel> Post(GenreInputModel input)
    {
        CheckModelState();

        Genre? entity = Mapper.Map<Genre>(input);
        if (Repository.Query().Any(x => x.Name == entity.Name))
        {
            throw new HttpResponseException(HttpStatusCode.Conflict, ResponseModel.CreateError("Genre already exists"));
        }
        Repository.Add(entity);
        return Ok(Mapper.Map<GenreOutputModel>(entity));
    }
    
    [HttpPut("{key}")]
    public override ActionResult<GenreOutputModel> Put(string key, GenreInputModel input)
    {
        CheckModelState();

        Genre? entityToUpdate = Repository.Get(key);
        CheckEntity(entityToUpdate);

        Mapper.Map(input, entityToUpdate);
        if (Repository.Query().Any(x => x.Name == entityToUpdate.Name && x.Id != entityToUpdate.Id))
        {
            throw new HttpResponseException(HttpStatusCode.Conflict, ResponseModel.CreateError("Genre already exists"));
        }
        Repository.Update(entityToUpdate);
        return Ok(Mapper.Map<GenreOutputModel>(entityToUpdate));
    }
}