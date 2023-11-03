using Microsoft.AspNetCore.Mvc;
using Yomikaze.Domain.Common;
using Yomikaze.Domain.Constants;
using Yomikaze.Domain.Database.Entities;
using Yomikaze.WebAPI.Models.Response;

namespace Yomikaze.WebAPI.Controllers;

[Route($"API/{Api.Version}/[controller]")]
[ApiController]
public class ComicsController : ControllerBase
{
    private readonly IDao<Comic> _comicDao;

    public ComicsController(IDao<Comic> comicDao)
    {
        _comicDao = comicDao;
    }

    [HttpGet]
    public async Task<ActionResult> GetComics()
    {
        return Ok(ResponseModel.CreateSuccess(await _comicDao.GetAllAsync()));
    }

    // get comic
    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseModel<Comic>>> GetComicAsync(long id)
    {
        var comic = await _comicDao.GetAsync(id);
        if (comic == null)
        {
            return NotFound(ResponseModel.CreateError($"Could not found comic with id '{id}'"));
        }
        return Ok(ResponseModel.CreateSuccess(comic));
    }

    // get comic chapters
    [HttpGet("{id}/Chapters")]
    public async Task<ActionResult<ResponseModel<IEnumerable<Chapter>>>> GetComicChaptersAsync(long id)
    {
        var comic = await _comicDao.GetAsync(id);
        if (comic == null)
        {
            return NotFound(ResponseModel.CreateError($"Could not found comic with id '{id}'"));
        }
        return Ok(ResponseModel.CreateSuccess(comic.Chapters.AsEnumerable()));
    }

    // get comic chapter
    [HttpGet("{id}/Chapters/{chapterIndex}")]
    public async Task<ActionResult<ResponseModel<Chapter>>> GetComicChapterAsync(long id, long chapterIndex)
    {
        var comic = await _comicDao.GetAsync(id);
        if (comic == null)
        {
            return NotFound(ResponseModel.CreateError($"Could not found comic with id '{id}'"));
        }
        var chapters = comic.Chapters.AsQueryable();
        var chapter = chapters.FirstOrDefault(c => c.Index == chapterIndex);
        if (chapter == null)
        {
            return NotFound(ResponseModel.CreateError($"Could not found chapter '{chapterIndex}' in comic with id '{id}'"));
        }
        return Ok(ResponseModel.CreateSuccess(chapter));
    }
}