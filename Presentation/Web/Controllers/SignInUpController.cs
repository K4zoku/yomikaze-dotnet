using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Yomikaze.Web.Models;

namespace Yomikaze.Web.Controllers;

public class SignInUpController : Controller
{

    public SignInUpController()
    {
       
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult SignUp()
    {
        return View();
    }

}