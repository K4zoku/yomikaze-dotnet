using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Yomikaze.API.Authentication;
using Yomikaze.Application.Helpers;
using Yomikaze.Application.Helpers.API;
using Yomikaze.Application.Helpers.Security;
using Yomikaze.Domain.Identity.Entities;
using Yomikaze.Infrastructure;
using Yomikaze.Infrastructure.Context;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
IConfiguration configuration = builder.Configuration;
Provider provider = Provider.FromName(configuration.GetValue("provider", Provider.Postgres.Name));
services.AddDbContext<YomikazeDbContext>(provider, configuration, "Yomikaze");
services.AddRouting(options => options.LowercaseUrls = true);
services
    .AddControllers(options =>
    {
        options.Filters.Add<HttpResponseExceptionFilter>();
    })
    .ConfigureApiBehaviorOptionsYomikaze()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);

services.AddPublicCors();

services.AddYomikazeIdentity();
services.UpgradePasswordSecurity().UseArgon2<User>();

JwtConfiguration jwt = configuration
                           .GetRequiredSection(JwtConfiguration.SectionName)
                           .Get<JwtConfiguration>()
                       ?? throw new InvalidOperationException("Could not read JWT Configuration");
services.AddSingleton(jwt);
services.AddJwtBearerAuthentication(jwt);
services.AddTransient<IAuthorizationHandler, SidValidationAuthorizationHandler>();

services.AddEndpointsApiExplorer();

services.AddSwaggerGenWithJwt();

WebApplication app = builder.Build();
IWebHostEnvironment env = app.Environment;

if (env.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Public");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Migrate and add default admin user if not exists
IServiceScope scope = app.Services.CreateScope();
IServiceProvider serviceProvider = scope.ServiceProvider;
configuration = app.Configuration;
YomikazeDbContext dbContext = serviceProvider.GetRequiredService<YomikazeDbContext>();
await dbContext.Database.MigrateAsync();
UserManager<User> userManager = serviceProvider.GetRequiredService<UserManager<User>>();
if (!userManager.Users.Any())
{
    User admin = new()
    {
        UserName = configuration["Admin:Username"] ?? "administrator",
        Email = configuration["Admin:Email"] ?? "administrator@yomikaze.org",
        EmailConfirmed = true
    };
    IdentityResult result = await userManager.CreateAsync(admin, configuration["Admin:Password"] ?? "Admin@123");
    if (!result.Succeeded)
    {
        throw new InvalidOperationException("Could not create default admin user");
    }
    result = await userManager.AddToRoleAsync(admin, "Administrator");
    if (!result.Succeeded)
    {
        throw new InvalidOperationException("Could not add default admin user to Administrator role");
    }
}

// Run the application
await app.RunAsync();