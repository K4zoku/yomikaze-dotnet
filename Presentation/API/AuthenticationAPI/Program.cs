using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Yomikaze.Application.Helpers;
using Yomikaze.Application.Helpers.API;
using Yomikaze.Application.Helpers.Database;
using Yomikaze.Application.Helpers.Security;
using Yomikaze.Domain.Identity.Entities;
using Yomikaze.Infrastructure.Database;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
ConfigurationManager configuration = builder.Configuration;

services.AddYomikazeDbContext(configuration);

services
    .AddControllers(options =>
    {
        options.Filters.Add<HttpResponseExceptionFilter>();
    })
    .ConfigureApiBehaviorOptionsYomikaze();

services.AddPublicCors();

services.AddYomikazeIdentity();

JwtConfiguration jwt = configuration
                           .GetRequiredSection(JwtConfiguration.SectionName)
                           .Get<JwtConfiguration>()
                       ?? throw new InvalidOperationException("Could not read JWT Configuration");
services.AddSingleton(jwt);
services.AddJwtBearerAuthentication(jwt);

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
var scope = app.Services.CreateScope();
var serviceProvider = scope.ServiceProvider;
var dbContext = serviceProvider.GetRequiredService<YomikazeDbContext>();
await dbContext.Database.MigrateAsync();
var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
if (!userManager.Users.Any())
{
    User admin = new()
    {
        UserName = "admin",
        Fullname = "Administrator",
        Email = "admin@yomikaze.org",
        EmailConfirmed = true
    };
    var result = await userManager.CreateAsync(admin, "Admin@123");
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