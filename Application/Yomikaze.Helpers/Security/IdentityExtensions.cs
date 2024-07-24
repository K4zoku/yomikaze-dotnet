using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Yomikaze.Domain.Entities;
using Yomikaze.Infrastructure.Context;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Yomikaze.Application.Helpers.Security;

public static class IdentityExtensions
{
    public static IServiceCollection AddYomikazeIdentity(this IServiceCollection services)
    {
        var builder = services.AddIdentity<User, Role>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.ClaimsIdentity.SecurityStampClaimType = JwtRegisteredClaimNames.Sid;
            options.ClaimsIdentity.UserIdClaimType = JwtRegisteredClaimNames.Sub;
            options.ClaimsIdentity.UserNameClaimType = JwtRegisteredClaimNames.Name;
            options.ClaimsIdentity.EmailClaimType = JwtRegisteredClaimNames.Email;
            options.ClaimsIdentity.RoleClaimType = "roles";
        });
        builder.AddEntityFrameworkStores<YomikazeDbContext>();
        builder.AddDefaultTokenProviders();
        return services;
    }
}