﻿using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Application.Helpers.API;

public static class UserExtensions
{
    public static string GetId(this ClaimsPrincipal user)
    {
        return user.FindFirstValue(ClaimTypes.NameIdentifier) ??
                       throw new HttpResponseException(500, "Cannot get user ID");
    }

    public static User GetUser(this ClaimsPrincipal user, UserManager<User> userManager)
    {
        Task<User?> task = userManager.GetUserAsync(user);
        task.Wait();
        return task.Result ?? throw new HttpResponseException(500, "Cannot get user");
    }
}