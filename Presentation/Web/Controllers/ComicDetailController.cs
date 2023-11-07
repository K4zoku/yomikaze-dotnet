using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Yomikaze.Web.Models;

namespace Yomikaze.Web.Controllers;

public class ComicDetailController : Controller
{

    public ComicDetailController()
    {
       
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Relate()
    {
        return View();
    }

}