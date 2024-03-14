using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Yomikaze.Domain.Helpers.Security;
using JwtBearerOptions = Yomikaze.Domain.Helpers.Security.JwtBearerOptions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
ConfigurationManager configuration = builder.Configuration;

services.AddControllers();
services.AddEndpointsApiExplorer();
IConfiguration ocelotConfig = configuration.GetRequiredSection("Ocelot");
services.AddOcelot(ocelotConfig);
services.AddSwaggerForOcelot(ocelotConfig, options =>
{
    options.AddAuthenticationProviderKeyMapping("Bearer", "JWT");
});

JwtConfiguration jwt = builder.Configuration
                           .GetRequiredSection(JwtConfiguration.SectionName)
                           .Get<JwtConfiguration>()
                       ?? throw new InvalidOperationException("Could not read JWT Configuration");
services.AddSingleton(jwt);

services
    .AddAuthentication(JwtAuthenticationOptions.Configure)
    .AddJwtBearer(options => JwtBearerOptions.Configure(options, jwt.SecurityKey));

services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("JWT", new JwtSecurityScheme());
    opt.AddSecurityRequirement(new JwtSecurityRequirement());
});

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