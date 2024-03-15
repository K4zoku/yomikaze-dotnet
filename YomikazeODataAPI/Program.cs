using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using Yomikaze.API.OData;
using Yomikaze.API.OData.Helpers;
using Yomikaze.Application.Data.Configs;
using Yomikaze.Application.Helpers.Database;
using Yomikaze.Application.Helpers.Security;
using Yomikaze.Domain.Entities;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
ConfigurationManager configuration = builder.Configuration;

services.AddYomikazeDbContext(configuration);

ODataConventionModelBuilder edm = new();
edm.EntitySet<Chapter>("Chapters");
edm.EntitySet<Comic>("Comics");
edm.EntitySet<Comment>("Comments");
edm.EntitySet<Genre>("Genres");

services.AddControllers()
    .AddOData(options => 
        options
            .Count()
            .Filter()
            .Expand()
            .Select()
            .OrderBy()
            .SetMaxTop(20)
            .AddRouteComponents("OData", edm.GetEdmModel()
    )
);

services.AddEndpointsApiExplorer();
services.AddSwaggerGen(options => options.OperationFilter<ODataOperationFilter>());
services.AddAutoMapper(typeof(MapperConfigs));
services.AddPublicCors();

WebApplication app = builder.Build();
IWebHostEnvironment env = app.Environment;

if (env.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseCors("Public");
app.Run();