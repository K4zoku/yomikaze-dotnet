using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;
using Yomikaze.API.Main.Base;
using Yomikaze.Application.Data.Repos;
using Yomikaze.Application.Helpers;
using Yomikaze.Application.Helpers.API;
using Yomikaze.Domain.Entities;
using Yomikaze.Domain.Models;

namespace Yomikaze.API.Main.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class CommentsController(DbContext dbContext, IMapper mapper)
    : CrudControllerBase<Comment, CommentInputModel, CommentOutputModel>(dbContext, mapper, new CommentRepo(dbContext))
{
    [HttpPost]
    public override ActionResult<CommentOutputModel> Post(CommentInputModel input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Comment? entity = Mapper.Map<Comment>(input);
        long id = User.GetId();
        entity.UserId = id;
        Repository.Add(entity);
        return Ok(Mapper.Map<CommentOutputModel>(entity));
    }

    [HttpPut("{key}")]
    public override ActionResult<CommentOutputModel> Put(long key, CommentInputModel input)
    {
        CheckModelState();

        Comment? entityToUpdate = Repository.Get(key);
        CheckEntity(entityToUpdate);

        long id = User.GetId();
        if (entityToUpdate.UserId != id) throw new HttpResponseException(HttpStatusCode.Forbidden, ResponseModel.CreateError("Forbidden"));

        Mapper.Map(input, entityToUpdate);
        Repository.Update(entityToUpdate);
        return Ok(Mapper.Map<CommentOutputModel>(entityToUpdate));
    }

    [HttpDelete("{key}")]
    public override ActionResult Delete(long key)
    {
        Comment? entity = Repository.Get(key);

        CheckEntity(entity);


        long id = User.GetId();
        if (entity.UserId != id && !User.HasClaim(ClaimTypes.Role, "Administrator")) throw new HttpResponseException(HttpStatusCode.Forbidden, ResponseModel.CreateError("Forbidden"));

        Repository.Delete(entity);
        return Ok();
    }
}