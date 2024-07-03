using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Web;
using Yomikaze.Application.Helpers.Security;
using Yomikaze.Infrastructure.Context;

namespace Yomikaze.API.Main.Services;

public class AuthenticationService(SignInManager<User> signInManager, JwtConfiguration jwtConfiguration, ILogger<AuthenticationService> logger)
{
    private SignInManager<User> SignInManager { get; } = signInManager;
    
    private UserManager<User> UserManager { get; } = signInManager.UserManager;
    
    private JwtConfiguration JwtConfiguration { get; } = jwtConfiguration;
    
    private ILogger<AuthenticationService> Logger { get; } = logger;
    
    public async Task<JwtSecurityToken> GenerateAccessToken(User user)
    {
        DateTimeOffset now = DateTimeOffset.UtcNow;
        List<Claim> claims =
        [
            new Claim(JwtRegisteredClaimNames.Jti, SnowflakeGenerator.Generate(31).ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
            new Claim(JwtRegisteredClaimNames.Exp, now.AddMinutes(JwtConfiguration.ExpireMinutes).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
        ];
        ClaimsPrincipal claimIdentity = await SignInManager.CreateUserPrincipalAsync(user);
        claims.AddRange(claimIdentity.Claims);
        return new JwtSecurityToken(signingCredentials: JwtConfiguration.SigningCredentials, claims: claims);
    }
    
    public async Task<User?> FindUser(string userNameOrEmail)
    {
        return await UserManager.FindByNameAsync(userNameOrEmail) ?? await UserManager.FindByEmailAsync(userNameOrEmail);
    }
    
    public async Task<User> GetOrCreateUser(ExternalLoginInfo info)
    {
        User? user = await UserManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

        if (user is not null)
        {
            return user;
        }

        var email = info.Principal.FindFirstValue(ClaimTypes.Email) ?? throw new InvalidOperationException("Email claim is missing.");
        user = await UserManager.FindByEmailAsync(email);
        if (user is null)
        {
            user = new User
            {
                UserName = email,
                Email = email,
                Name = info.Principal.FindFirstValue(ClaimTypes.Name) ?? email,   
                EmailConfirmed = true
            };
            var result = await UserManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Failed to create a user.");
            }
            await UserManager.AddToRoleAsync(user, YomikazeDbContext.Default.DefaulRole.Name!);
        }
        await UserManager.AddLoginAsync(user, info);
        return user;
    }

    public ActionResult CallbackOrResult(string? returnUrl, JwtSecurityToken token)
    {
        if (string.IsNullOrWhiteSpace(returnUrl))
        {
            return new OkObjectResult(new TokenModel(token.ToTokenString()));
        }

        try
        {
            UriBuilder builder = new(returnUrl);
            var query = HttpUtility.ParseQueryString(builder.Query);
            query.Set("token", token.ToTokenString());
            builder.Query = query.ToString();
            return new RedirectResult(builder.ToString());
        }
        catch (UriFormatException)
        {
            return new OkObjectResult(new TokenModel(token.ToTokenString()));
        }
    }
    
}