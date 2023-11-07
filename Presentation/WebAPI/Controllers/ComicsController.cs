using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yomikaze.Domain.Common;
using Yomikaze.Domain.Constants;
using Yomikaze.Domain.Database.Entities;
using Yomikaze.WebAPI.Models;
using Yomikaze.WebAPI.Models.Common;
using Yomikaze.WebAPI.Models.Response;

namespace Yomikaze.WebAPI.Controllers;

[Route($"API/{Api.Version}/[controller]")]
[ApiController]
public class ComicsController : ControllerBase
{
    private readonly IDao<Comic> _comicDao;
    private readonly IDao<Genre> _genreDao;

    public ComicsController(IDao<Comic> comicDao, IDao<Genre> genreDao)
    {
        _comicDao = comicDao;
        _genreDao = genreDao;
    }

    [HttpGet]
    public async Task<ActionResult<ResponseModel<ComicModel>>> GetComics()
    {
        return Ok(ResponseModel.CreateSuccess((await _comicDao.GetAllAsync()).Select(c => c.ToModel(false)).ToList()));
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
        return Ok(ResponseModel.CreateSuccess(comic.ToModel(false)));
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
        var chapters = comic.Chapters.OrderBy(c => c.Index).Select(c => c.ToModel(false));
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

    [HttpGet("Search/By-Genre/{genre}")]
    public async Task<ResponseModel<IEnumerable<ComicModel>>> SearchByGenreAsync(string genre)
    {
        var comics = await _comicDao.QueryAsync();
        IEnumerable<Comic> filtered;
        if (long.TryParse(genre, out long id))
        {
            filtered = comics.Where(c => c.Genres.Any(g => g.Id == id) || c.Genres.Any(g => g.Name == genre));
        }
        else
        {
            filtered = comics.Where(c => c.Genres.Any(g => g.Name == genre));
        }
        return ResponseModel.CreateSuccess(filtered.Select(c => c.ToModel(false)));
    }

    [HttpGet("Search/By-Name/{name}")]
    public async Task<ResponseModel<IEnumerable<ComicModel>>> SearchByNameAsync(string name)
    {
        var comics = await _comicDao.QueryAsync();
        name = name.ToLower();
        var filtered = await comics.Where(c => c.Name.ToLower().Contains(name)).ToListAsync();
        return ResponseModel.CreateSuccess(filtered.Select(c => c.ToModel(false)));
    }

    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<ResponseModel<ComicModel>> Create([FromBody] ComicModel model)
    {
        var comic = new Comic
        {
            Name = model.Name,
            Description = model.Description,
            Cover = model.Cover,
            Authors = model.Authors,
            Aliases = model.Aliases,
            Banner = model.Banner,
            Published = model.Published,
            Ended = model.Ended,
            Genres = model.Genres.Select(genre => _genreDao.GetAsync(genre.Id).Result).ToList(),
        };
        await _comicDao.AddAsync(comic);
        _comicDao.SaveChanges();
        return ResponseModel.CreateSuccess(comic.ToModel());
    }
}