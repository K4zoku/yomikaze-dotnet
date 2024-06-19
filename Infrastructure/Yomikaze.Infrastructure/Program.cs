using Microsoft.EntityFrameworkCore;
using Yomikaze.Infrastructure;
using Yomikaze.Infrastructure.Context;
using static Yomikaze.Infrastructure.Provider;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
ConfigurationManager configuration = builder.Configuration;

Provider provider = FromName(configuration.GetValue("provider", Postgres.Name));

services.AddDbContext<YomikazeDbContext>(provider, configuration, "Yomikaze");

WebApplication app = builder.Build();
ILogger logger = app.Logger;

logger.LogInformation("Getting services...");
await using AsyncServiceScope scope = app.Services.CreateAsyncScope();
IServiceProvider servicesProvider = scope.ServiceProvider;

logger.LogInformation("Migrating databases...");
logger.LogInformation($"Migrating {nameof(YomikazeDbContext)}...");
YomikazeDbContext yomikazeDbContext = servicesProvider.GetRequiredService<YomikazeDbContext>();
await yomikazeDbContext.Database.MigrateAsync();
logger.LogInformation("Databases migrated successfully.");  

logger.LogInformation("Done.");
