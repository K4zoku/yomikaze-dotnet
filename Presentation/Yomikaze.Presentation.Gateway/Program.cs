using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Yomikaze.Application.Helpers.Security;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
ConfigurationManager configuration = builder.Configuration;

services.AddControllers();
services.AddEndpointsApiExplorer();


IConfiguration ocelotConfig = configuration.GetRequiredSection("Ocelot");
services.AddOcelot(ocelotConfig);
services.AddSwaggerForOcelot(ocelotConfig);

JwtConfiguration jwt = builder.Configuration
                           .GetRequiredSection(JwtConfiguration.SectionName)
                           .Get<JwtConfiguration>()
                       ?? throw new InvalidOperationException("Could not read JWT Configuration");
services.AddSingleton(jwt);
services.AddRouting(options => options.LowercaseUrls = true);

services.AddSwaggerGenWithJwt();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerForOcelotUI(options => options.PathToSwaggerGenerator = "/swagger/docs");
}

await app.UseOcelot();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();