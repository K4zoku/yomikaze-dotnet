using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using Yomikaze.Application.Helpers;
using Yomikaze.Application.Helpers.Security;
using Yomikaze.Domain.Abstracts;
using Yomikaze.Domain.Identity.Entities;
using Yomikaze.Domain.Identity.Models;
using Yomikaze.Domain.Models;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Yomikaze.API.Authentication.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AuthenticationController(
    RoleManager<Role> roleManager,
    SignInManager<User> signInManager,
    ILogger<AuthenticationController> logger,
    JwtConfiguration jwtConfiguration)
    : ControllerBase
{
    private UserManager<User> UserManager { get; } = signInManager.UserManager;
    private ClaimsIdentityOptions ClaimsIdentity { get; } = signInManager.Options.ClaimsIdentity;
    private RoleManager<Role> RoleManager { get; } = roleManager;
    private SignInManager<User> SignInManager { get; } = signInManager;
    private ILogger<AuthenticationController> Logger { get; } = logger;

    private JwtConfiguration Jwt { get; } = jwtConfiguration;

    [HttpPost]
    public async Task<ActionResult<TokenModel>> SignIn([FromBody] SignInModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        User user = await SignInManager.UserManager.FindByNameAsync(model.Username) ??
                    await SignInManager.UserManager.FindByEmailAsync(model.Username) ??
                    throw new HttpResponseException(HttpStatusCode.NotFound,
                        ResponseModel.CreateError("User not found"));

        SignInResult result = await SignInManager.CheckPasswordSignInAsync(user, model.Password, true);
        if (result.Succeeded)
        {
            JwtSecurityToken token = await GenerateToken(user);
            return new TokenModel(token.ToTokenString());
        }

        if (result.IsNotAllowed)
        {
            Logger.LogWarning("User is not allowed to sign in.");
            ModelState.AddModelError(nameof(model.Username), "User is not allowed to sign in.");
        }
        else if (result.RequiresTwoFactor)
        {
            Logger.LogWarning("Two factor authentication required.");
            // TODO)) Implement two factor authentication
            // response temporary token to be used for two factor authentication (contains user id)
            return new TokenModel(user.Id.ToString());
        }
        else if (result.IsLockedOut)
        {
            Logger.LogWarning("User is locked out. Lockout end in: {}s",
                (user.LockoutEnd?.DateTime - DateTime.UtcNow)?.TotalSeconds);
            ModelState.AddModelError(nameof(model.Username), "User is locked out.");
        }
        else
        {
            ModelState.AddModelError(nameof(model.Password), "Invalid password.");
            Logger.LogWarning("Invalid login attempt.");
        }

        return ValidationProblem(ModelState);
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

        user = new User
        {
            UserName = model.Username, 
            Email = model.Email, 
            Name = model.Fullname, 
            Birthday = model.Birthday
        };

        IdentityResult result = await UserManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            string token = (await GenerateToken(user)).ToTokenString();
            // TODO)) gRPC service call to create UserProfile on another service
            await UserManager.AddLoginAsync(user, new UserLoginInfo("YomikazeToken", token, "YomikazeToken"));
            return ResponseModel.CreateSuccess(new TokenModel(token));
        }

        result.Errors.Where(e => e.Code.Contains("Password")).ToList()
            .ForEach(e => ModelState.AddModelError("Password", e.Description));
        result.Errors.Where(e => e.Code.Contains("Email")).ToList()
            .ForEach(e => ModelState.AddModelError("Email", e.Description));
        result.Errors.Where(e => e.Code.Contains("Username")).ToList()
            .ForEach(e => ModelState.AddModelError("Username", e.Description));

        return ValidationProblem(ModelState);
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<ResponseModel>> Info()
    {
        User? user = await UserManager.GetUserAsync(User);
        if (user is null)
        {
            return Problem("User not found", statusCode: (int)HttpStatusCode.NotFound, title: "Not Found",
                type: "https://tools.ietf.org/html/rfc7231#section-6.5.4");
        }

        bool isAdmin = await UserManager.IsInRoleAsync(user, "Administrator");
        return Ok(ResponseModel.CreateSuccess("Authorized",
            new
            {
                User = new { user.Id, user.UserName, user.Email, IsAdmin = isAdmin },
                Claims = User.Claims.ToDictionary(c => c.Type, c => c.Value)
            }
        ));
    }

    [NonAction]
    private async Task<JwtSecurityToken> GenerateToken(User user)
    {
        DateTimeOffset now = DateTimeOffset.UtcNow;
        List<Claim> claims =
        [
            new Claim(JwtRegisteredClaimNames.Jti, SnowflakeGenerator.Generate(31).ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
            // new Claim(JwtRegisteredClaimNames.Exp, now.AddMinutes(Jwt.ExpireMinutes).ToUnixTimeSeconds().ToString(),
                // ClaimValueTypes.Integer64)
        ];
        ClaimsPrincipal claimIdentity = await SignInManager.CreateUserPrincipalAsync(user);
        claims.AddRange(claimIdentity.Claims);
        return new JwtSecurityToken(signingCredentials: Jwt.SigningCredentials, claims: claims);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Validate()
    {
        User? user = await SignInManager.ValidateSecurityStampAsync(User);
        if (user is not null)
        {
            return Ok();
        }

        Logger.LogWarning("Invalid security stamp");
        return Problem("Invalid security stamp", statusCode: (int)HttpStatusCode.Unauthorized, title: "Unauthorized",
            type: "https://tools.ietf.org/html/rfc7235#section-3.1");
    }

    [Authorize(Roles = "Administrator")]
    [HttpGet]
    [ActionName("authorize-admin")]
    public IActionResult Admin()
    {
        return Ok();
    }
}