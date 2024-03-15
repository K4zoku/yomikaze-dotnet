using Microsoft.Extensions.DependencyInjection;

namespace Yomikaze.Application.Helpers.Security;

public static class CorsExtensions
{
    public static IServiceCollection AddPublicCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("Public", cors =>
            {
                cors.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .SetIsOriginAllowed(_ => true);
            });
        });
        return services;
    }
}