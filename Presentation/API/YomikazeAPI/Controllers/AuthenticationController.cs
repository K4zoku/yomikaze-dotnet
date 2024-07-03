using Google.Apis.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using Yomikaze.Application.Helpers.Security;
using static Yomikaze.Infrastructure.Context.YomikazeDbContext.Default;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using AuthenticationService = Yomikaze.API.Main.Services.AuthenticationService;

namespace Yomikaze.API.Main.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController(
    SignInManager<User> signInManager,
    AuthenticationService service,
    ILogger<AuthenticationController> logger)
    : ControllerBase
{
    private UserManager<User> UserManager { get; } = signInManager.UserManager;
    private SignInManager<User> SignInManager { get; } = signInManager;
    
    private AuthenticationService Service { get; } = service;
    private ILogger<AuthenticationController> Logger { get; } = logger;

    [HttpPost]
    [Route("[action]")]
    public async Task<ActionResult<TokenModel>> Login([FromBody] LoginModel model)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        User? user = await Service.FindUser(model.Username);
        
        if (user is null)
        {
            ModelState.AddModelError(nameof(model.Username), "User not found.");
            return ValidationProblem(ModelState);
        }

        SignInResult result = await SignInManager.CheckPasswordSignInAsync(user, model.Password, true);
        if (HandleSignInResult(result))
        {
            JwtSecurityToken token = await Service.GenerateAccessToken(user);
            return new TokenModel(token.ToTokenString());
        }

        if (!result.RequiresTwoFactor)
        {
            return ValidationProblem(ModelState);
        }

        if (string.IsNullOrEmpty(model.TwoFactorCode))
        {
            Logger.LogWarning("User requires two factor authentication.");
            ModelState.AddModelError(nameof(model.TwoFactorCode), "User requires two factor authentication.");
        }
        else
        {
            result = await SignInManager.TwoFactorSignInAsync("default", model.TwoFactorCode, false, false);
            ModelState.Clear(); 
            if (HandleSignInResult(result))
            {
                JwtSecurityToken token = await Service.GenerateAccessToken(user);
                return new TokenModel(token.ToTokenString());
            }

            if (!result.RequiresTwoFactor)
            {
                return ValidationProblem(ModelState);
            }

            Logger.LogWarning("Invalid two factor code.");
            ModelState.AddModelError(nameof(model.TwoFactorCode), "Invalid two factor code.");
        }

        return ValidationProblem(ModelState);
    }
    
    [HttpGet]
    [Route($"{nameof(Login)}/external")]
    public async Task<ActionResult> ExternalLoginProviders()
    {
        IEnumerable<AuthenticationScheme> schemes = await SignInManager.GetExternalAuthenticationSchemesAsync();
        return Ok(schemes.Select(s => new { s.Name, s.DisplayName }));
    }

    [HttpGet]
    [Route($"{nameof(Login)}/external/{{provider}}")]
    public ActionResult ExternalLogin([FromRoute] string provider, [FromQuery] string? returnUrl = null)
    {
        string? redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Authentication", new { provider, returnUrl });
        AuthenticationProperties properties = SignInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        return Challenge(properties, provider);
    }
    
    [HttpGet]
    [Route($"{nameof(Login)}/callback/{{provider}}")]
    public async Task<ActionResult> ExternalLoginCallback([FromRoute] string provider, [FromQuery] string? returnUrl = null, string? remoteErrors = null)
    {
        ExternalLoginInfo? info = await SignInManager.GetExternalLoginInfoAsync();
        if (info is null)
        {
            ModelState.AddModelError("Provider", "External login info not found.");
            return ValidationProblem(ModelState);
        }

        User user = await Service.GetOrCreateUser(info);
        
        SignInResult result = await SignInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false, true);

        if (!HandleSignInResult(result))
        {
            return ValidationProblem(ModelState);
        }

        JwtSecurityToken token = await Service.GenerateAccessToken(user);
        return Service.CallbackOrResult(returnUrl, token);
    }
    
    [HttpPost($"{nameof(Login)}/external/Google")]
    public async Task<ActionResult> LoginWithGoogleToken([FromBody] GoogleTokenModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        GoogleJsonWebSignature.Payload? payload = await GoogleJsonWebSignature.ValidateAsync(model.Token);
        if (payload is null)
        {
            ModelState.AddModelError("Token", "Invalid token.");
            return ValidationProblem(ModelState);
        }
        ClaimsPrincipal? principal = new(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Email, payload.Email),
            new Claim(ClaimTypes.Name, payload.Name),
        }, "Google"));
        ExternalLoginInfo info = new(principal, "Google", payload.Subject, "Google");
        User user = await Service.GetOrCreateUser(info);
        
        SignInResult result = await SignInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false, true);

        if (!HandleSignInResult(result))
        {
            return ValidationProblem(ModelState);
        }

        JwtSecurityToken token = await Service.GenerateAccessToken(user);
        return Ok(new TokenModel(token.ToTokenString()));
    }

    private bool HandleSignInResult(SignInResult result)
    {
        if (result.Succeeded)
        {
            return true;
        }
        if (result.IsLockedOut)
        {
            ModelState.AddModelError("Username", "User is locked out.");
        } 
        else if (result.IsNotAllowed)
        {
            ModelState.AddModelError("Username", "User is not allowed to sign in.");
        }
        else
        {
            ModelState.AddModelError("Password", "Password is incorrect.");
        }
        return false;
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<ActionResult<TokenModel>> Register([FromBody] RegisterModel model)
    {
        User? user = await Service.FindUser(model.Username);
        if (user is not null)
        {
            ModelState.AddModelError("Username", "Username may already exists.");
            ModelState.AddModelError("Email", "Email may already exists.");
            return Conflict(ModelState);
        }

        user = new User
        {
            UserName = model.Username,
            Email = model.Email,
            Name = model.Fullname,
            Birthday = model.Birthday.ToUniversalTime()
        };

        IdentityResult result = await UserManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            await UserManager.AddToRoleAsync(user, DefaulRole.Name!);
            string token = (await Service.GenerateAccessToken(user)).ToTokenString();
            return Ok(new TokenModel(token));
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
    [Route("[action]")]
    public async Task<ActionResult<ProfileModel>> Info()
    {
        User? user = await UserManager.GetUserAsync(User);
        if (user is null)
        {
            return Problem("User not found", statusCode: (int)HttpStatusCode.NotFound, title: "Not Found",
                type: "https://tools.ietf.org/html/rfc7231#section-6.5.4");
        }
        
        return Ok(
            new
            {
                User = (ProfileModel)user,
                Claims = User.Claims.GroupBy(c => c.Type).ToDictionary(g => g.Key, g => g.Select(c => c.Value))
            }
        );
    }

}