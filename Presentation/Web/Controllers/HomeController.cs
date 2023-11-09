using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Yomikaze.Application.Data.Models;
using Yomikaze.Domain.Common;
using Yomikaze.Domain.Database.Entities;
using Yomikaze.Web.Models;

namespace Yomikaze.Web.Controllers;

public class HomeController : Controller
{
    private readonly IDao<Comic> _comicDao;

    public HomeController(IDao<Comic> comicDao)
    {
        _comicDao = comicDao;
    }

    public async Task<IActionResult> Index()
    {   
        var comics = await _comicDao.GetAllAsync();
        return View(comics.Select(comic => comic.ToModel()));
    }

    public IActionResult Category()
    {
        return View();
    }

    public IActionResult History()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}