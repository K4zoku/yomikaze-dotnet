using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Yomikaze.AuthenApi.Services;

namespace Yomikaze.AuthenApi.Helpers;

public static class UserHelpers
{
    public static long GetId(this ClaimsPrincipal principal)
    {
        var claim = principal.FindFirst(c => c.Type == JwtRegisteredClaimNames.NameId || c.Type == ClaimTypes.NameIdentifier) ?? throw new ApiServiceException("User id not found");
        if (!long.TryParse(claim.Value, out long id)) throw new ApiServiceException("User id is invalid");
        return id;
    }
}