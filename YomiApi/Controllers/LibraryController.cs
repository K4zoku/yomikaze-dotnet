using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Yomikaze.Application.Data.Models.Common;
using Yomikaze.Application.Data.Models.Response;
using Yomikaze.Domain.Common;
using Yomikaze.Domain.Entities;
using Yomikaze.Domain.Helpers;

namespace YomiApi.Controllers;

[ApiController]
[Route("[controller]")]
public class LibraryController : ControllerBase
{
    protected IMapper Mapper { get; set; }
    protected IRepo<LibraryEntry> Repository { get; set; }

    protected LibraryController(IMapper mapper, IRepo<LibraryEntry> repository)
    {
        Mapper = mapper;
        Repository = repository;
    }


    [HttpPost]
    [Authorize]
    public virtual ActionResult<ResponseModel> Post(long comicId)
    {
       
        // check if comic in user's library
        var idStr = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new HttpResponseException(500, "Cannot get user ID");
        if (!long.TryParse(idStr, out long id)) throw new HttpResponseException(500, "Cannot parse user ID");

        var entity = Repository.Query().FirstOrDefault(x => x.ComicId == comicId && x.UserId == id);
        if (entity != null) return BadRequest("Comic already in library");

        entity = new LibraryEntry { ComicId = comicId, UserId = id };
        Repository.Add(entity);

        return Ok(ResponseModel.CreateSuccess("Add successful"));
    }

    [HttpDelete]
    [Route("{comicId}")]
    [Authorize]
    public virtual ActionResult Delete(long comicId)
    {
        var idStr = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new HttpResponseException(500, "Cannot get user ID");
        if (!long.TryParse(idStr, out long id)) throw new HttpResponseException(500, "Cannot parse user ID");

        var entity = Repository.Query().FirstOrDefault(x => x.ComicId == comicId && x.UserId == id);
        if (entity == null) return NotFound();
        Repository.Delete(entity);
        return Ok();
    }

}
