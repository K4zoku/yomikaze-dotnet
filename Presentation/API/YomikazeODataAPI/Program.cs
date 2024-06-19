using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Batch;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using System.Reflection;
using Yomikaze.API.OData.Helpers;
using Yomikaze.Application.Helpers.Security;
using Yomikaze.Domain.Entities;
using Yomikaze.Infrastructure;
using Yomikaze.Infrastructure.Context;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
ConfigurationManager configuration = builder.Configuration;
Provider provider = Provider.FromName(configuration.GetValue("provider", Provider.SqlServer.Name));
services.AddDbContext<YomikazeDbContext>(provider, configuration, "Yomikaze");
services.AddScoped<DbContext, YomikazeDbContext>();
services.AddYomikazeIdentity();

JwtConfiguration jwt = configuration
                           .GetRequiredSection(JwtConfiguration.SectionName)
                           .Get<JwtConfiguration>()
                       ?? throw new InvalidOperationException("Could not read JWT Configuration");
services.AddSingleton(jwt);
services.AddJwtBearerAuthentication(jwt);

ODataConventionModelBuilder modelBuilder = new();
modelBuilder.EntitySet<Chapter>("Chapters");
modelBuilder.EntitySet<Comic>("Comics");
modelBuilder.EntitySet<Comment>("Comments");
modelBuilder.EntitySet<Tag>("Tags");
modelBuilder.EntitySet<LibraryEntry>("Library");
modelBuilder.EntitySet<HistoryRecord>("History");
modelBuilder.EnableLowerCamelCase();
foreach(StructuralTypeConfiguration? type in modelBuilder.StructuralTypes)
{
    PropertyInfo? property = type.ClrType.GetProperty("IdStr");
    if (property is not null) type.AddProperty(property);
}

services.AddControllers()
    .AddOData(options =>
        options
            .Count()
            .Filter()
            .Expand()
            .Select()
            .OrderBy()
            .SetMaxTop(null)
            .AddRouteComponents("API/OData", modelBuilder.GetEdmModel(), new DefaultODataBatchHandler())
    );

services.AddEndpointsApiExplorer();
services.AddSwaggerGenWithJwt();
services.AddSwaggerGen(options =>
{
    options.OperationFilter<ODataOperationFilter>();
});
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

app.UseODataBatching();
app.UseODataQueryRequest();

app.MapControllers();

app.UseCors("Public");
app.Run();