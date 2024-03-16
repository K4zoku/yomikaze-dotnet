using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using Yomikaze.Application.Helpers;
using Yomikaze.Application.Helpers.API;
using Yomikaze.Application.Helpers.Security;
using Yomikaze.Domain.Entities;
using Yomikaze.Domain.Models;

namespace Yomikaze.API.Authentication.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AuthenticationController(UserManager<User> userManager, RoleManager<Role> roleManager, JwtConfiguration jwtConfiguration)
    : ControllerBase
{
    private UserManager<User> UserManager { get; } = userManager;
    private RoleManager<Role> RoleManager { get; } = roleManager;

    private JwtConfiguration Jwt { get; } = jwtConfiguration;

    [HttpPost]
    public async Task<ActionResult<ResponseModel<TokenModel>>> SignIn([FromBody] SignInModel model)
    {
        User user = await UserManager.FindByNameAsync(model.Username) ??
                    throw new HttpResponseException(HttpStatusCode.NotFound,
                        ResponseModel.CreateError("User not found"));

        bool passwordMatched = await UserManager.CheckPasswordAsync(user, model.Password);
        if (!passwordMatched)
        {
            throw new HttpResponseException(HttpStatusCode.Unauthorized,
                ResponseModel.CreateError("Password does not match"));
        }

        string token = (await GenerateToken(user)).ToTokenString();
        return ResponseModel.CreateSuccess(new TokenModel(token));
    }

    [HttpPost]
    public async Task<ActionResult<ResponseModel<TokenModel>>> SignUp([FromBody] SignUpModel model)
    {
        bool any = UserManager.Users.Any();
        User? user;
        if (any) // If there is any user, check if username or email already exists
        {
            user = await UserManager.FindByNameAsync(model.Username) ??
                         await UserManager.FindByEmailAsync(model.Email);
            if (user is not null)
            {
                throw new HttpResponseException(HttpStatusCode.Conflict,
                    ResponseModel.CreateError("Username or email already exists"));
            }   
        }

        user = new User { UserName = model.Username, Email = model.Email, Birthday = model.Birthday.Date };

        IdentityResult result = await UserManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            throw new HttpResponseException(HttpStatusCode.InternalServerError,
                ResponseModel.CreateError("Errors occurred!", result.Errors));
        }

        if (!any) // First user is admin
        {
            await UserManager.AddToRoleAsync(user, "Administrator");
        }
        return ResponseModel.CreateSuccess(new TokenModel((await GenerateToken(user)).ToTokenString()));

    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<ResponseModel>> Info()
    {
        User? user = User.GetUser(userManager);
        return Ok(ResponseModel.CreateSuccess("Authorized",
            new { Profile = user, Claims = User.Claims.ToDictionary(c => c.Type, c => c.Value) }
        ));
    }

    [NonAction]
    private async Task<JwtSecurityToken> GenerateToken(User user)
    {
        List<Claim> claims = [new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())];
        IEnumerable<Claim> roles = (await UserManager.GetRolesAsync(user)).Select(r => new Claim(ClaimTypes.Role, r));
        claims.AddRange(roles);
        return new JwtSecurityToken(signingCredentials: Jwt.SigningCredentials, claims: claims);
    }
}