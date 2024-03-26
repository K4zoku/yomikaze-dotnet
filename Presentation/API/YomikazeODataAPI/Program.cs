using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using Yomikaze.API.OData.Helpers;
using Yomikaze.Application.Helpers.Security;
using Yomikaze.Domain.Entities;
using Yomikaze.Infrastructure;
using Yomikaze.Infrastructure.Context;
using Yomikaze.Infrastructure.Context.Identity;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
ConfigurationManager configuration = builder.Configuration;
Provider provider = Provider.FromName(configuration.GetValue("provider", Provider.SqlServer.Name));
services.AddDbContext<YomikazeIdentityDbContext>(provider, configuration, "YomikazeIdentity");
services.AddDbContext<YomikazeDbContext>(provider, configuration, "Yomikaze");
services.AddScoped<DbContext, YomikazeDbContext>();
services.AddYomikazeIdentity();

JwtConfiguration jwt = configuration
                           .GetRequiredSection(JwtConfiguration.SectionName)
                           .Get<JwtConfiguration>()
                       ?? throw new InvalidOperationException("Could not read JWT Configuration");
services.AddSingleton(jwt);
services.AddJwtBearerAuthentication(jwt);

ODataConventionModelBuilder edm = new();
edm.EntitySet<Chapter>("Chapters");
edm.EntitySet<Comic>("Comics");
edm.EntitySet<Comment>("Comments");
edm.EntitySet<Genre>("Genres");
edm.EntitySet<LibraryEntry>("Library");
edm.EntitySet<HistoryRecord>("History");

services.AddControllers()
    .AddOData(options =>
        options
            .Count()
            .Filter()
            .Expand()
            .Select()
            .OrderBy()
            .SetMaxTop(null)
            .AddRouteComponents("API/OData", edm.GetEdmModel()
            )
    );

services.AddEndpointsApiExplorer();
services.AddSwaggerGenWithJwt();
services.AddSwaggerGen(options => options.OperationFilter<ODataOperationFilter>());
services.AddPublicCors();

WebApplication app = builder.Build();
IWebHostEnvironment env = app.Environment;

if (env.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors("Public");
app.Run();