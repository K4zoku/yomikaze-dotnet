using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Yomikaze.Domain.Identity.Entities;

namespace Yomikaze.Application.Helpers.API;

public static class UserExtensions
{
    public static string GetIdString(this ClaimsPrincipal user)
    {
        return user.FindFirstValue(JwtRegisteredClaimNames.NameId) ?? user.FindFirstValue(JwtRegisteredClaimNames.Sub) ??
               throw new HttpResponseException(500, "Cannot get user ID");
    }

    public static ulong GetId(this ClaimsPrincipal user)
    {
        string idStr = GetIdString(user);
        if (ulong.TryParse(idStr, out ulong id))
        {
            return id;
        }

        throw new HttpResponseException(500, "Cannot parse user ID");
    }

    public static User GetUser(this ClaimsPrincipal user, UserManager<User> userManager)
    {
        Task<User?> task = userManager.GetUserAsync(user);
        task.Wait();
        return task.Result ?? throw new HttpResponseException(500, "Cannot get user");
    }
}