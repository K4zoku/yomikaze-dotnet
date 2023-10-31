using Microsoft.AspNetCore.Mvc;
using Yomikaze.Domain.Common;
using Yomikaze.Domain.Constants;
using Yomikaze.Domain.Database.Entities;

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

    // get comic
    [HttpGet("{id}")]
    public async Task<ActionResult<Comic>> GetComicAsync(long id)
    {
        var comic = await _comicDao.GetAsync(id);
        if (comic == null)
        {
            return NotFound();
        }
        return comic;
    }

    // get comic chapters
    [HttpGet("{id}/chapters")]
    public async Task<ActionResult<IEnumerable<Chapter>>> GetComicChaptersAsync(long id)
    {
        var comic = await _comicDao.GetAsync(id);
        if (comic == null)
        {
            return NotFound();
        }
        return comic.Chapters.ToList();
    }

    // get comic chapter
    [HttpGet("{id}/chapters/{chapterId}")]
    public async Task<ActionResult<Chapter>> GetComicChapterAsync(long id, long chapterId)
    {
        var comic = await _comicDao.GetAsync(id);
        if (comic == null)
        {
            return NotFound();
        }
        var chapters = comic.Chapters.AsQueryable();
        var chapter = chapters.FirstOrDefault(c => c.Id == chapterId);
        if (chapter == null)
        {
            return NotFound();
        }
        return chapter;
    }
}