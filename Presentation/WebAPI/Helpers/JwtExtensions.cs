using System.IdentityModel.Tokens.Jwt;

namespace Yomikaze.WebAPI.Helpers;

public static class JwtExtensions
{
    public static string ToTokenString(this JwtSecurityToken token) => new JwtSecurityTokenHandler().WriteToken(token);
}

