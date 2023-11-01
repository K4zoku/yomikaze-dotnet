using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Yomikaze.WebAPI.Helpers;

public static class UserHelpers
{
    public static string? GetId(this ClaimsPrincipal principal)
    {
        var claim = principal.FindFirst(c => c.Type == JwtRegisteredClaimNames.NameId);
        return claim?.Value;
    }
}