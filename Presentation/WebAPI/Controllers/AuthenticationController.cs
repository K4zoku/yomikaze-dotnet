using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Yomikaze.Domain.Database.Entities.Identity;
using Yomikaze.WebAPI.Models;

namespace Yomikaze.WebAPI.Controllers;

[ApiController]
[Route("API/[controller]")]
public class AuthenticationController : Controller
{
    private UserManager<YomikazeUser> UserManager { get; }
    private SignInManager<YomikazeUser> SignInManager { get; }



    private IConfiguration Configuration { get; }

    private SymmetricSecurityKey Key { get; }

    public AuthenticationController(UserManager<YomikazeUser> userManager, SignInManager<YomikazeUser> signInManager, IConfiguration configuration, SymmetricSecurityKey key)
    {
        UserManager = userManager;
        SignInManager = signInManager;
        Configuration = configuration;
        Key = key;
    }

    [HttpPost]
    public async Task<IActionResult> Token([FromBody] UsernamePasswordModel model)
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

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, Configuration["Jwt:Subject"] ?? "AccessToken"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
            new Claim("Id", user.Id.ToString()),
        };
        var signIn = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            Configuration["Jwt:Issuer"],
            Configuration["Jwt:Audience"],
            claims,
            signingCredentials: signIn
        );
        return Ok(new AuthModel { Token = new JwtSecurityTokenHandler().WriteToken(token), Success = true, Message = "Authentication successful" });
    }

    [HttpPost]
    [Route("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await UserManager.FindByNameAsync(model.Username) ?? await UserManager.FindByEmailAsync(model.Email);
        if (user != null)
        {
            return BadRequest(new ResponseModel { Success = false, Message = "Account is already exist" });
        }

        if (!model.Password.Equals(model.ConfirmPassword))
        {
            return BadRequest(new ResponseModel { Success = false, Message = "Password and confirm password not match" });
        }

        user = new YomikazeUser() { UserName = model.Username, Email = model.Email, Birthday = model.Birthday };

        await UserManager.CreateAsync(user, model.Password);


        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, Configuration["Jwt:Subject"] ?? "AccessToken"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
            new Claim("Id", user.Id.ToString()),
        };
        var signIn = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            Configuration["Jwt:Issuer"],
            Configuration["Jwt:Audience"],
            claims,
            signingCredentials: signIn
        );
        return Ok(new AuthModel { Token = new JwtSecurityTokenHandler().WriteToken(token), Success = true, Message = "Authentication successful" });

    }


}