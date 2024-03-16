using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        User? user = await UserManager.FindByNameAsync(model.Username) ??
                     await UserManager.FindByEmailAsync(model.Email);
        if (user is not null)
        {
            throw new HttpResponseException(HttpStatusCode.Conflict,
                ResponseModel.CreateError("Username or email already exists"));
        }   
        
        user = new User { UserName = model.Username, Email = model.Email, Birthday = model.Birthday.Date };

        IdentityResult result = await UserManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            string token = (await GenerateToken(user)).ToTokenString();
            return ResponseModel.CreateSuccess(new TokenModel(token));
        }

        result.Errors.Where(e => e.Code.Contains("Password")).ToList()
            .ForEach(e => ModelState.AddModelError("Password", e.Description));
        result.Errors.Where(e => e.Code.Contains("Email")).ToList()
            .ForEach(e => ModelState.AddModelError("Email", e.Description));
        result.Errors.Where(e => e.Code.Contains("Username")).ToList()
            .ForEach(e => ModelState.AddModelError("Username", e.Description));
        
        Dictionary<string, IEnumerable<string>> errors = new();
        foreach ((string? key, ModelStateEntry? value) in ModelState)
        {
            IEnumerable<string> errorsToAdd = value.Errors
                .Where(error => !string.IsNullOrEmpty(error.ErrorMessage))
                .Select(error => error.ErrorMessage);
            errors.Add(key, errorsToAdd);
        }

        ResponseModel<object, Dictionary<string, IEnumerable<string>>> problems =
            ResponseModel.CreateError("Validation errors", errors);
        throw new HttpResponseException(HttpStatusCode.BadRequest, problems);
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