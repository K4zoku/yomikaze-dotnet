using Microsoft.AspNetCore.Mvc;
using Yomikaze.Domain.Common;
using Yomikaze.Domain.Constants;
using Yomikaze.Domain.Database.Entities;
using Yomikaze.WebAPI.Helpers;
using Yomikaze.WebAPI.Models.Common;
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
    public async Task<ActionResult<ResponseModel<ComicModel>>> GetComicAsync(long id)
    {
        var comic = await _comicDao.GetAsync(id);
        if (comic == null)
        {
            return NotFound(ResponseModel.CreateError($"Could not found comic with id '{id}'"));
        }
        return Ok(ResponseModel.CreateSuccess(comic.ToModel()));
    }

    // get comic chapters
    [HttpGet("{id}/Chapters")]
    public async Task<ActionResult<ResponseModel<IEnumerable<ChapterModel>>>> GetComicChaptersAsync(long id)
    {
        var comic = await _comicDao.GetAsync(id);
        if (comic == null)
        {
            return NotFound(ResponseModel.CreateError($"Could not found comic with id '{id}'"));
        }
        var chapters = comic.Chapters.Select(c => c.ToModel());
        return Ok(ResponseModel.CreateSuccess(chapters));
    }

    // get comic chapter
    [HttpGet("{id}/Chapters/{chapterIndex}")]
    public async Task<ActionResult<ResponseModel<ChapterModel>>> GetComicChapterAsync(long id, long chapterIndex)
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
        return Ok(ResponseModel.CreateSuccess(chapter.ToModel()));
    }
}