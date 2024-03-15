using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yomikaze.Application.Helpers.API;
using Yomikaze.Domain.Common;
using Yomikaze.Domain.Entities;
using Yomikaze.Domain.Models.Response;

namespace Yomikaze.API.Main.Controllers;

[ApiController]
[Route("[controller]")]
public class LibraryController : ControllerBase
{
    protected LibraryController(IMapper mapper, IRepo<LibraryEntry> repository)
    {
        Mapper = mapper;
        Repository = repository;
    }

    protected IMapper Mapper { get; set; }
    protected IRepo<LibraryEntry> Repository { get; set; }


    [HttpPost]
    [Authorize]
    public virtual ActionResult<ResponseModel> Post(long comicId)
    {
        long id = User.GetId();
        LibraryEntry? entity = Repository.Query().FirstOrDefault(x => x.ComicId == comicId && x.UserId == id);
        if (entity != null)
        {
            return BadRequest("Comic already in library");
        }

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
        LibraryEntry? entity = Repository.Query().FirstOrDefault(x => x.ComicId == comicId && x.UserId == id);
        if (entity == null)
        {
            return NotFound();
        }

        Repository.Delete(entity);
        return Ok();
    }
}