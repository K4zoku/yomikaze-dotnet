using System.IdentityModel.Tokens.Jwt;

namespace Yomikaze.Domain.Helpers.Security;

public static class JwtExtensions
{
    public static string ToTokenString(this JwtSecurityToken token)
    {
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}