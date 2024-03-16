using Abstracts;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Yomikaze.Application.Data.Repos;
using Yomikaze.Application.Helpers;
using Yomikaze.Application.Helpers.API;
using Yomikaze.Domain.Entities;
using Yomikaze.Domain.Models;

namespace Yomikaze.API.Main.Controllers;

[ApiController]
[Route("[controller]")]
public class LibraryController(IMapper mapper, DbContext dbContext) : ControllerBase
{
    protected IMapper Mapper { get; set; } = mapper;
    protected LibraryRepo Repository { get; set; } = new LibraryRepo(dbContext);




    [HttpPost]
    [Authorize]
    public virtual ActionResult<ResponseModel> Post(long comicId)
    {
        long id = User.GetId();
        LibraryEntry? entity = Repository.GetLibraryEntry(id, comicId);
        if (entity != null) throw new HttpResponseException(HttpStatusCode.Conflict, ResponseModel.CreateError("Comic already in library"));
        entity = new LibraryEntry { ComicId = comicId, UserId = id };
        Repository.Add(entity);
        return Ok(ResponseModel.CreateSuccess("Add successful"));
    }

    [HttpDelete]
    [Route("{comicId:long}")]
    [Authorize]
    public virtual ActionResult Delete(long comicId)
    {
        long id = User.GetId();
        LibraryEntry entity = Repository.GetLibraryEntry(id, comicId) ?? throw new HttpResponseException(HttpStatusCode.NotFound, ResponseModel.CreateError("Comic not in library"));
        Repository.Delete(entity);
        return Ok();
    }
}