using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Yomikaze.WebAPI;

public static class UserHelpers
{
    public static string? GetId(this ClaimsPrincipal principal)
    {
        var userIdClaim = principal.FindFirst(c => c.Type == JwtRegisteredClaimNames.NameId);
        if (userIdClaim != null && !string.IsNullOrEmpty(userIdClaim.Value))
        {
            return userIdClaim.Value;
        }

        return null;
    }
}