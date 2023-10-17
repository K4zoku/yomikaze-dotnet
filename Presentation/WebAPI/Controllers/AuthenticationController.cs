using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Yomikaze.Domain.Database.Entities.Identity;
using Yomikaze.WebAPI.Models;

namespace Yomikaze.WebAPI.Controllers;

[ApiController]
[Route("API/[controller]")]
public class AuthenticationController : Controller
{
    private UserManager<YomikazeUser> UserManager { get; }
    private SignInManager<YomikazeUser> SignInManager { get; }
    
    public AuthenticationController(UserManager<YomikazeUser> userManager, SignInManager<YomikazeUser> signInManager)
    {
        UserManager = userManager;
        SignInManager = signInManager;
    }
    
    [HttpPost]
    public async Task<IActionResult> Authenticate([FromBody] UsernamePasswordModel model)
    {
        var user = await UserManager.FindByNameAsync(model.Username);
        if (user == null)
        {
            return Unauthorized(new ResponseModel { Success = false, Message = "User not found" });
        }
        var result = await SignInManager.CheckPasswordSignInAsync(user, model.Password, false);
        if (!result.Succeeded)
        {
            return Unauthorized(new ResponseModel { Success = false, Message = "Password is incorrect" });
        }
        var token = await UserManager.GenerateUserTokenAsync(user, "Default", "Access");
        return Ok(new AuthModel { Token = token });
    }
    
}