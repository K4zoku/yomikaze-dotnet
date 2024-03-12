using Microsoft.AspNetCore.Identity;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Yomikaze.Application.Data.Models.Request;
using Yomikaze.Application.Data.Models.Response;
using Yomikaze.Application.Helpers;
using Yomikaze.Domain.Database.Entities.Identity;
using Yomikaze.AuthenApi.Helpers;

namespace Yomikaze.AuthenApi.Services;

public class AuthenticationService
{
    private UserManager<User> UserManager { get; }

    private JwtConfiguration Jwt { get; }

    public AuthenticationService(UserManager<User> userManager, JwtConfiguration jwtConfiguration)
    {
        UserManager = userManager;
        Jwt = jwtConfiguration;
        
    }

    public async Task<TokenModel> SignIn(SignInModel model)
    {
        var user = await UserManager.FindByNameAsync(model.Username) ?? throw new ApiServiceException("User not found!");
        var passwordMatched = await UserManager.CheckPasswordAsync(user, model.Password);
        if (!passwordMatched) throw new ApiServiceException("Password does not match");

        var token = (await GetToken(user)).ToTokenString();
        return new TokenModel(token);
    }

    public async Task<TokenModel> SignUp(SignUpModel model)
    {
        var user = await UserManager.FindByNameAsync(model.Username) ?? await UserManager.FindByEmailAsync(model.Email);
        if (user is not null) throw new ApiServiceException("Username or email already exists");

        user = new User
        {
            UserName = model.Username,
            Email = model.Email,
            Birthday = model.Birthday
        };

        var result = await UserManager.CreateAsync(user, model.Password);
        if (result.Succeeded) return new TokenModel(await GetToken(user));

        throw new ApiServiceException(result.Errors.Select(error => error.Description));
    }


    private JwtSecurityToken GetToken(IEnumerable<Claim> claims)
    {
        JwtSecurityToken token = new(
            signingCredentials: Jwt.SigningCredentials,
            claims: claims
        );
        return token;
    }

    private async Task<JwtSecurityToken> GetToken(User user)
    {
        var roles = (await UserManager.GetRolesAsync(user)).Select(r => new Claim(ClaimTypes.Role, r));
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
            new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString())
        };
        claims.AddRange(roles);
        return GetToken(claims);
    }
}
