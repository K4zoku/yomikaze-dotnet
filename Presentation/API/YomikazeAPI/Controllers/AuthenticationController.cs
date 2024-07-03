using Google.Apis.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Web;
using Yomikaze.Application.Helpers.Security;
using static Yomikaze.Infrastructure.Context.YomikazeDbContext.Default;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Yomikaze.API.Main.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController(
    RoleManager<Role> roleManager,
    SignInManager<User> signInManager,
    AuthenticatorTokenProvider<User> authenticatorTokenProvider,
                
    ILogger<AuthenticationController> logger,
    JwtConfiguration jwtConfiguration)
    : ControllerBase
{
    private UserManager<User> UserManager { get; } = signInManager.UserManager;
    private ClaimsIdentityOptions ClaimsIdentity { get; } = signInManager.Options.ClaimsIdentity;
    private RoleManager<Role> RoleManager { get; } = roleManager;
    private AuthenticatorTokenProvider<User> AuthenticatorTokenProvider { get; } = authenticatorTokenProvider;
    private SignInManager<User> SignInManager { get; } = signInManager;
    private ILogger<AuthenticationController> Logger { get; } = logger;

    private JwtConfiguration Jwt { get; } = jwtConfiguration;

    [HttpPost]
    [Route("[action]")]
    public async Task<ActionResult<TokenModel>> Login([FromBody] LoginModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        User? user = await SignInManager.UserManager.FindByNameAsync(model.Username) ??
                    await SignInManager.UserManager.FindByEmailAsync(model.Username);
        
        if (user is null)
        {
            ModelState.AddModelError(nameof(model.Username), "User not found.");
            return ValidationProblem(ModelState);
        }

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
            if (!string.IsNullOrEmpty(model.TwoFactorCode))
            {
                result = await SignInManager.TwoFactorSignInAsync("default", model.TwoFactorCode, false, false);
                if (result.Succeeded)
                {
                    JwtSecurityToken token = await GenerateToken(user);
                    return new TokenModel(token.ToTokenString());
                }
                Logger.LogWarning("Invalid two factor code.");
                ModelState.AddModelError(nameof(model.TwoFactorCode), "Invalid two factor code.");
            }
            else
            {
                Logger.LogWarning("User requires two factor authentication.");
                ModelState.AddModelError(nameof(model.TwoFactorCode), "User requires two factor authentication.");
            }
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
            return Problem("External login info not found.", statusCode: (int)HttpStatusCode.NotFound, title: "Not Found",
                type: "https://tools.ietf.org/html/rfc7231#section-6.5.4");
        }
        var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        if (string.IsNullOrWhiteSpace(email))
        {
            ModelState.AddModelError("Email", "Email not found.");
        }

        var name = info.Principal.FindFirstValue(ClaimTypes.Name);
        if (string.IsNullOrWhiteSpace(name))
        {
            ModelState.AddModelError("Name", "Name not found.");
        }
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        User? user = await UserManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
        if (user is null) // if we find by login provider failed, try to find by email
        {
            user = await UserManager.FindByEmailAsync(email!); 
            if (user is null) // if user by email not found, create new user and add login
            {
                user = new User
                {
                    UserName = email,
                    Email = email,
                    Name = name!,
                    EmailConfirmed = true,
                };
                IdentityResult result = await UserManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    result.Errors.Where(e => e.Code.Contains("Email")).ToList()
                        .ForEach(e => ModelState.AddModelError("Email", e.Description));
                    result.Errors.Where(e => e.Code.Contains("Username")).ToList()
                        .ForEach(e => ModelState.AddModelError("Username", e.Description));
                    return ValidationProblem(ModelState);
                }    
                if (!string.IsNullOrWhiteSpace(DefaulRole.Name))
                {
                    await UserManager.AddToRoleAsync(user, DefaulRole.Name);
                }
            }
            
            await UserManager.AddLoginAsync(user, info);
        }
        
        if (await UserManager.IsLockedOutAsync(user))
        {
            ModelState.AddModelError("Username", "User is locked out.");
            return ValidationProblem(ModelState);   
        }
            
        await SignInManager.SignInAsync(user, false, info.LoginProvider);
    
        JwtSecurityToken token = await GenerateToken(user);
        if (string.IsNullOrWhiteSpace(returnUrl))
        {
            return Ok(new TokenModel(token.ToTokenString()));
        }

        try
        {
            UriBuilder builder = new(returnUrl);
            var query = HttpUtility.ParseQueryString(builder.Query);
            query.Set("token", token.ToTokenString());
            builder.Query = query.ToString();
            return Redirect(builder.ToString());
        }
        catch (UriFormatException)
        {
            ModelState.AddModelError("ReturnUrl", "Invalid return url.");
            return BadRequest(ModelState);
        }
    }
    
    [HttpPost($"{nameof(Login)}/external/google/token")]
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
        
        User? user = await UserManager.FindByLoginAsync("Google", payload.Subject);
        if (user is null)
        {
            user = await UserManager.FindByEmailAsync(payload.Email);
            if (user is null)
            {
                user = new User
                {
                    UserName = payload.Email,
                    Email = payload.Email,
                    Name = payload.Name,
                    EmailConfirmed = true,
                };
                IdentityResult result = await UserManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    result.Errors.Where(e => e.Code.Contains("Email")).ToList()
                        .ForEach(e => ModelState.AddModelError("Email", e.Description));
                    result.Errors.Where(e => e.Code.Contains("Username")).ToList()
                        .ForEach(e => ModelState.AddModelError("Username", e.Description));
                    return ValidationProblem(ModelState);
                }    
                if (!string.IsNullOrWhiteSpace(DefaulRole.Name))
                {
                    await UserManager.AddToRoleAsync(user, DefaulRole.Name);
                }
            }
            await UserManager.AddLoginAsync(user, new UserLoginInfo("Google", payload.Subject, "Google"));
        }
        
        if (await UserManager.IsLockedOutAsync(user))
        {
            ModelState.AddModelError("Username", "User is locked out.");
            return ValidationProblem(ModelState);   
        }
        
        await SignInManager.SignInAsync(user, false, "Google");
        
        JwtSecurityToken token = await GenerateToken(user);
        
        return Ok(new TokenModel(token.ToTokenString()));
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<ActionResult<TokenModel>> Register([FromBody] RegisterModel model)
    {
        User? user = await UserManager.FindByNameAsync(model.Username) ??
                     await UserManager.FindByEmailAsync(model.Email);
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
            if (!string.IsNullOrWhiteSpace(DefaulRole.Name))
            {
                await UserManager.AddToRoleAsync(user, DefaulRole.Name);
            }
            string token = (await GenerateToken(user)).ToTokenString();
            
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
                Claims = User.Claims.ToDictionary(c => c.Type, c => c.Value)
            }
        );
    }

    [NonAction]
    private async Task<JwtSecurityToken> GenerateToken(User user)
    {
        DateTimeOffset now = DateTimeOffset.UtcNow;
        List<Claim> claims =
        [
            new Claim(JwtRegisteredClaimNames.Jti, SnowflakeGenerator.Generate(31).ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
            new Claim(JwtRegisteredClaimNames.Exp, now.AddMinutes(Jwt.ExpireMinutes).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
        ];
        ClaimsPrincipal claimIdentity = await SignInManager.CreateUserPrincipalAsync(user);
        claims.AddRange(claimIdentity.Claims);
        return new JwtSecurityToken(signingCredentials: Jwt.SigningCredentials, claims: claims);
    }

}