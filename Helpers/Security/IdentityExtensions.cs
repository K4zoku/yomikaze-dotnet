using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Yomikaze.Domain.Entities.Identity;
using Yomikaze.Infrastructure.Database;

namespace Yomikaze.Application.Helpers.Security;

public static class IdentityExtensions
{
    public static IServiceCollection AddYomikazeIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<YomikazeDbContext>()
            .AddDefaultTokenProviders();
        return services;
    }
}