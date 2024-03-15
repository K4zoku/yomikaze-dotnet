using Yomikaze.Application.Helpers.API;
using Yomikaze.Application.Helpers.Database;
using Yomikaze.Application.Helpers.Security;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
ConfigurationManager configuration = builder.Configuration;

services.AddYomikazeDbContext(configuration);

services.AddControllers().ConfigureApiBehaviorOptionsYomikaze();

services.AddYomikazeIdentity();

JwtConfiguration jwt = configuration
                           .GetRequiredSection(JwtConfiguration.SectionName)
                           .Get<JwtConfiguration>()
                       ?? throw new InvalidOperationException("Could not read JWT Configuration");
services.AddSingleton(jwt);
services.AddJwtBearerAuthentication(jwt);

services.AddEndpointsApiExplorer();
services.AddSwaggerGenWithJwt();
services.AddPublicCors();

var app = builder.Build();
IWebHostEnvironment env = app.Environment;

if (env.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("Public");

app.MapControllers();

app.Run();
