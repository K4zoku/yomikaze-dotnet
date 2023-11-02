using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Yomikaze.Web.Models;

namespace Yomikaze.Web.Controllers;

public class PasswordController : Controller
{

    public PasswordController()
    {
       
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ResetPassword()
    {
        return View();
    }

}