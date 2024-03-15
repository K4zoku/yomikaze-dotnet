using Microsoft.EntityFrameworkCore;
using Yomikaze.Domain.Helpers;
using Yomikaze.Domain.Helpers.API;
using Yomikaze.Domain.Helpers.Security;
using Yomikaze.Infrastructure.Database;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
ConfigurationManager configuration = builder.Configuration;

services.AddDbContext<YomikazeDbContext>(options =>
{
    string? connectionString = configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString, server => server.EnableRetryOnFailure());
});
services.AddScoped<DbContext, YomikazeDbContext>();

services.AddPublicCors();

services.AddYomikazeIdentity();

JwtConfiguration jwt = configuration
                           .GetRequiredSection(JwtConfiguration.SectionName)
                           .Get<JwtConfiguration>()
                       ?? throw new InvalidOperationException("Could not read JWT Configuration");

services.AddSingleton(jwt);

services.AddJwtBearerAuthentication(jwt);

services
    .AddControllers(options =>
    {
        options.Filters.Add<HttpResponseExceptionFilter>();
    })
    .ConfigureApiBehaviorOptionsYomikaze();

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
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();