using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yomikaze.API.Main.Base;
using Yomikaze.Application.Data.Repos;
using Yomikaze.Domain.Entities;
using Yomikaze.Domain.Models;

namespace Yomikaze.API.Main.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Administrator")]
public class ComicsController(DbContext dbContext, IMapper mapper)
    : CrudControllerBase<Comic, ComicInputModel, ComicOutputModel>(dbContext, mapper, new ComicRepo(dbContext))
{

    [HttpPost]
    public override ActionResult<ComicOutputModel> Post(ComicInputModel input)
    {
        CheckModelState();

        Comic? entity = Mapper.Map<Comic>(input);
        Repository.Add(entity);
        return Ok(Mapper.Map<ComicOutputModel>(entity));
    }

    [HttpPut("{key}")]
    public override ActionResult<ComicOutputModel> Put(long key, ComicInputModel input)
    {
        CheckModelState();

        Comic? entityToUpdate = Repository.Get(key);
        CheckEntity(entityToUpdate);

        Mapper.Map(input, entityToUpdate);
        Repository.Update(entityToUpdate);

        return Ok(Mapper.Map<ComicOutputModel>(entityToUpdate));
    }


    [HttpDelete("{key}")]
    public override ActionResult Delete(long key)
    {
        Comic? entity = Repository.Get(key);
        CheckEntity(entity);

        Repository.Delete(entity);
        return Ok();
    }
}