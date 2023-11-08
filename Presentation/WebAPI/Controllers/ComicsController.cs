using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yomikaze.Application.Data.Models;
using Yomikaze.Application.Data.Models.Common;
using Yomikaze.Application.Data.Models.Request;
using Yomikaze.Application.Data.Models.Response;
using Yomikaze.Domain.Common;
using Yomikaze.Domain.Constants;
using Yomikaze.Domain.Database.Entities;

namespace Yomikaze.WebAPI.Controllers;

[Route($"API/{Api.Version}/[controller]")]
[ApiController]
public class ComicsController : ControllerBase
{
    private readonly IDao<Comic> _comicDao;
    private readonly IDao<Genre> _genreDao;
    private readonly IDao<Chapter> _chapterDao;
    private readonly IDao<Page> _pageDao;

    public ComicsController(IDao<Comic> comicDao, IDao<Genre> genreDao, IDao<Page> pageDao, IDao<Chapter> chapterDao)
    {
        _comicDao = comicDao;
        _genreDao = genreDao;
        _pageDao = pageDao;
        _chapterDao = chapterDao;
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
    public async Task<ActionResult<ResponseModel<ComicModel>>> Create([FromBody] ComicRequestModel model)
    {
        var genres = model.Genres.Split(",").Select(g => g.Trim()).ToList();
        var existingGenres = await (await _genreDao.QueryAsync())
            .Where(g => genres.Contains(g.Name))
            .ToListAsync();
        var newGenres = genres.Except(existingGenres.Select(g => g.Name)).ToList();
        foreach (var genre in newGenres)
        {
            var genreEntity = new Genre { Name = genre };
            await _genreDao.AddAsync(genreEntity);
            existingGenres.Add(genreEntity);
        }
        _genreDao.SaveChanges();

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
            Genres = existingGenres,
        };
        await _comicDao.AddAsync(comic);
        _comicDao.SaveChanges();
        return Ok(ResponseModel.CreateSuccess(comic.ToModel()));
    }

    [HttpPost("{id}/Chapters/Add")]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<ResponseModel<ChapterModel>>> AddChapter([FromRoute] long id, [FromBody] ChapterRequestModel model)
    {
        var comic = await _comicDao.GetAsync(id);
        if (comic == null)
        {
            return NotFound(ResponseModel.CreateError($"Could not found comic with id '{id}'"));
        }
        var pages = model.Pages.Split("\n").Select(p => new Page { Image = p.Trim() }).ToList();
        foreach (var page in pages)
        {
            await _pageDao.AddAsync(page);
        }

        var chapter = new Chapter
        {
            Title = model.Title,
            Comic = comic,
            Description = model.Description,
            Available = DateTimeOffset.UtcNow,
            Pages = pages,
        };
        await _chapterDao.AddAsync(chapter);
        comic.Chapters.Add(chapter);
        await _comicDao.UpdateAsync(comic);
        _comicDao.SaveChanges();

        return Ok(ResponseModel.CreateSuccess(chapter.ToModel()));
    }

}