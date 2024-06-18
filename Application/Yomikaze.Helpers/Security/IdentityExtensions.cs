using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;
using Yomikaze.Domain.Identity.Entities;
using Yomikaze.Infrastructure.Context.Identity;

namespace Yomikaze.Application.Helpers.Security;

public static class IdentityExtensions
{
    public static IServiceCollection AddYomikazeIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.ClaimsIdentity.SecurityStampClaimType = JwtRegisteredClaimNames.Sid;
                options.ClaimsIdentity.UserIdClaimType = JwtRegisteredClaimNames.Sub;
                options.ClaimsIdentity.UserNameClaimType = JwtRegisteredClaimNames.Name;
                options.ClaimsIdentity.EmailClaimType = JwtRegisteredClaimNames.Email;
                options.ClaimsIdentity.RoleClaimType = "roles";
            })
            .AddEntityFrameworkStores<YomikazeIdentityDbContext>()
            .AddDefaultTokenProviders();
        return services;
    }
}