using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
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
            })
            .AddEntityFrameworkStores<YomikazeIdentityDbContext>()
            .AddDefaultTokenProviders();
        return services;
    }
}