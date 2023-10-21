using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Yomikaze.Domain.Database.Entities.Identity;
using Yomikaze.WebAPI.Helpers;
using Yomikaze.WebAPI.Models;

namespace Yomikaze.WebAPI.Controllers;

[ApiController]
[Route($"API/V{Version}/[controller]")]
public class AuthenticationController : ControllerBase
{
    public const string Version = "1";
    private UserManager<YomikazeUser> UserManager { get; }
    private RoleManager<YomikazeRole> RoleManager { get; }

    private JwtConfiguration JwtConfiguration { get; }

    public AuthenticationController(UserManager<YomikazeUser> userManager, RoleManager<YomikazeRole> roleManager, JwtConfiguration jwtConfiguration)
    {
        UserManager = userManager;
        RoleManager = roleManager;
        JwtConfiguration = jwtConfiguration;
    }

    [HttpPost]
    [Route("SignIn")]
    public async Task<IActionResult> SignIn([FromBody] SignInRequest model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new Response
            {
                Success = false,
                Message = "Validation failed, please check input",
                Data = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
            });
        }
        var user = await UserManager.FindByNameAsync(model.Username);
        if (user == null)
        {
            return Unauthorized(new Response
            {
                Success = false,
                Message = "User not found"
            });
        }
        var result = await UserManager.CheckPasswordAsync(user, model.Password);
        if (!result)
        {
            return Unauthorized(new Response
            {
                Success = false,
                Message = "Password not match"
            });
        }

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
            new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
        };
        var token = GetToken(claims).ToTokenString();
        return Ok(new SignInResponse
        {
            Token = token,
            Success = true,
            Message = "Authentication successful"
        });
    }

    [HttpPost]
    [Route("SignUp")]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new Response
            {
                Success = false,
                Message = "Validation failed, please check input",
                Data = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
            });
        }

        var user = await UserManager.FindByNameAsync(model.Username) ?? await UserManager.FindByEmailAsync(model.Email);
        if (user != null)
        {
            return BadRequest(new Response
            {
                Success = false,
                Message = "Account is already exist"
            });
        }

        if (!model.Password.Equals(model.ConfirmPassword))
        {
            return BadRequest(new Response
            {
                Success = false,
                Message = "Password and confirm password not match"
            });
        }

        user = new YomikazeUser() { UserName = model.Username, Email = model.Email, Birthday = model.Birthday };

        var result = await UserManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            // return 500
            return StatusCode(500, new Response
            {
                Success = false,
                Message = "There was error while creating account",
                Data = result.Errors.Select(e => e.Description)
            });
        }

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        };
        var token = GetToken(claims).ToTokenString();
        return Ok(new SignInResponse
        {
            Token = token,
            Success = true,
            Message = "Create account successful"
        });

    }

    [NonAction]
    private JwtSecurityToken GetToken(IEnumerable<Claim> claims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConfiguration.Secret));
        var token = new JwtSecurityToken(
            issuer: JwtConfiguration.Issuer,
            audience: JwtConfiguration.Audience,
            claims: claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );
        return token;
    }

    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
        return Ok(value: new Response
        {
            Success = true,
            Message = "Authorized",
            Data = new
            {
                Id = User.GetId(),
                Claims = User.Claims.Select(c => new Dictionary<string, string> { { c.Type, c.Value } })
            }
        });
    }

}