using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Yomikaze.Application.Data.Models;
using Yomikaze.Application.Data.Models.Common;
using Yomikaze.Domain.Common;
using Yomikaze.Domain.Database.Entities;
using Yomikaze.Web.Models;

namespace Yomikaze.Web.Controllers;

[Controller]
[Route("[controller]")]
public class ChapterController : Controller
{

    private readonly IDao<Comic> _comicDao;
    private readonly IDao<Genre> _genreDao;
    private readonly IDao<Chapter> _chapterDao;
    private readonly IDao<Page> _pageDao;

    public ChapterController(IDao<Comic> comicDao, IDao<Genre> genreDao, IDao<Page> pageDao, IDao<Chapter> chapterDao)
    {
        _comicDao = comicDao;
        _genreDao = genreDao;
        _pageDao = pageDao;
        _chapterDao = chapterDao;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Index(int id)
    {
        var chapter =  await _chapterDao.GetAsync(id);
        ViewBag.Chapters = chapter.Comic.Chapters.AsEnumerable();
        return View(chapter.ToModel());
    }

    public IActionResult Relate()
    {
        return View();
    }

}