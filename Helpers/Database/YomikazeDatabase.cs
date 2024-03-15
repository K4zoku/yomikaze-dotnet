using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Yomikaze.Infrastructure.Database;

namespace Yomikaze.Application.Helpers.Database;

public static class YomikazeDatabase
{
    public static void AddYomikazeDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<YomikazeDbContext>(options =>
        {
            string? connectionString = configuration.GetConnectionString("DefaultConnection") 
                                    ?? throw new InvalidOperationException("Could not read connection string");
            options.UseSqlServer(connectionString, server => server.EnableRetryOnFailure());
        });
        services.AddScoped<DbContext, YomikazeDbContext>();
    }
}