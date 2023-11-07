using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Yomikaze.Web.Models;

namespace Yomikaze.Web.Controllers;

public class ProfileController : Controller
{

    public ProfileController()
    {
       
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult EditProfile()
    {
        return View();
    }

}