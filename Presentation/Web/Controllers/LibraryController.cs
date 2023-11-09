using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;
using Yomikaze.Application.Data.Models;
using Yomikaze.Application.Data.Models.Common;
using Yomikaze.Domain.Common;
using Yomikaze.Domain.Database.Entities;
using Yomikaze.Domain.Database.Entities.Identity;
using Yomikaze.Web.Models;

namespace Yomikaze.Web.Controllers;

[Controller]
[Route("[controller]")]
public class LibraryController : Controller
{

    private readonly IDao<Comic> _comicDao;
    private readonly IDao<Genre> _genreDao;
    private readonly IDao<Chapter> _chapterDao;
    private readonly IDao<LibraryEntry> _libraryDao;
    private readonly UserManager<User> _userManager;

    public LibraryController(IDao<Comic> comicDao, IDao<Genre> genreDao, IDao<LibraryEntry> libraryDao, IDao<Chapter> chapterDao, UserManager<User> userManager)
    {
        _comicDao = comicDao;
        _genreDao = genreDao;
        _libraryDao = libraryDao;
        _chapterDao = chapterDao;
        _userManager = userManager;
    }
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        var library = (await _libraryDao.QueryAsync())
            .Include(l => l.Comic)
            .Where(l => l.UserId == user.Id).ToList();

        return View(library.DistinctBy(l => l.ComicId).Select(l => l.Comic.ToModel()));
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> Add(long id)
    {
        var user = await _userManager.GetUserAsync(User);
        var comic = await _comicDao.GetAsync(id);
        if (comic == null)
        {
            return NotFound();
        }
        var library = (await _libraryDao.QueryAsync())
            .Include(l => l.Comic)
            .Where(l => l.UserId == user.Id && l.ComicId == id).FirstOrDefault();
        if (library != null)
        {
            await _libraryDao.DeleteAsync(library);
            _libraryDao.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        await _libraryDao.AddAsync(new LibraryEntry
        {
            ComicId = id,
            UserId = user.Id
        });
        _libraryDao.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

}