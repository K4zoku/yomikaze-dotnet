using Microsoft.EntityFrameworkCore;
using Yomikaze.Infrastructure;
using Yomikaze.Infrastructure.Context;
using Yomikaze.Infrastructure.Context.Identity;
using static Yomikaze.Infrastructure.Provider;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

var provider = FromName(configuration.GetValue("provider", Sqlite.Name));

services.AddDbContext<YomikazeIdentityDbContext>(provider, configuration, "YomikazeIdentity"); 

services.AddDbContext<YomikazeDbContext>(provider, configuration, "Yomikaze");

var app = builder.Build();
var logger = app.Logger;

logger.LogInformation("Getting services...");
await using var scope = app.Services.CreateAsyncScope();
var servicesProvider = scope.ServiceProvider;

logger.LogInformation("Migrating databases...");
logger.LogInformation($"Migrating {nameof(YomikazeIdentityDbContext)}...");
var identityDbContext = servicesProvider.GetRequiredService<YomikazeIdentityDbContext>();
await identityDbContext.Database.MigrateAsync();

logger.LogInformation($"Migrating {nameof(YomikazeDbContext)}...");
var yomikazeDbContext = servicesProvider.GetRequiredService<YomikazeDbContext>();
await yomikazeDbContext.Database.MigrateAsync();
logger.LogInformation("Databases migrated successfully.");  

logger.LogInformation("Done.");
