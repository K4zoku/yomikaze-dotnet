using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Yomikaze.Domain.Entities.Identity;
using Yomikaze.WebAPI.Models;

namespace Yomikaze.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : Controller
{
    private UserManager<YomikazeUser> UserManager;
    private SignInManager<YomikazeUser> SignInManager;
    
    public AuthenticationController(UserManager<YomikazeUser> userManager, SignInManager<YomikazeUser> signInManager)
    {
        UserManager = userManager;
        SignInManager = signInManager;
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] UsernamePasswordModel model)
    {
        var user = await UserManager.FindByNameAsync(model.Username);
        if (user == null)
        {
            return Unauthorized();
        }
        var result = await SignInManager.CheckPasswordSignInAsync(user, model.Password, false);
        if (!result.Succeeded)
        {
            return Unauthorized();
        }
        // var token = await UserManager.GenerateUserTokenAsync(user, "Default", "Token");
        return Ok(new AuthModel { Token = "Token" });
    }
    
}