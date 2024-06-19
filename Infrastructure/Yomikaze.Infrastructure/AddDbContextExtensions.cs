using Microsoft.EntityFrameworkCore;

namespace Yomikaze.Infrastructure;

public static class AddDbContextExtensions
{
    public static void AddDbContext<T>(this IServiceCollection services, Provider provider,
        IConfiguration configuration, string key) where T : DbContext
    {
        string connectionString = provider.GetConnectionString(configuration, key);
        services.AddDbContext<T>(provider, connectionString);
    }

    public static void AddDbContext<T>(this IServiceCollection services, Provider provider, string connectionString)
        where T : DbContext
    {
        services.AddDbContext<T>(options =>
        {
            switch (provider.Name)
            {
                case "Postgres":
                    options.UseNpgsql(connectionString, x =>
                    {
                        x.MigrationsAssembly(provider.Assembly);
                        x.EnableRetryOnFailure();
                        x.MigrationsHistoryTable("migrations_history", "efcore");
                    }).UseSnakeCaseNamingConvention();
                    break;
                case "SqlServer":
                    options
                        .UseSqlServer(connectionString, x => x.MigrationsAssembly(provider.Assembly));
                    break;
                default:
                    throw new ArgumentException("Invalid provider");
            }
        });
    }
}