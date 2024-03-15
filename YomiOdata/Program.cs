using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Yomikaze.Application.Data.Configs;
using Yomikaze.Domain.Entities;
using Yomikaze.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);
ConfigureBuilder(builder);
var app = builder.Build();
Configure(app);
app.Run();
return;


static void ConfigureBuilder(WebApplicationBuilder builder)
{
    var services = builder.Services;
    var configuration = builder.Configuration;
    var env = builder.Environment;

    services.AddControllers()
        .AddOData(options => options.Count().Filter().Expand().Select().OrderBy().SetMaxTop(100)
            .AddRouteComponents("OData", GetEdmModel()));
    services.AddEndpointsApiExplorer();


    if (env.IsDevelopment())
        services.AddSwaggerGen(options =>
        {

            //options.OperationFilter<ODataOperationFilter>();
        });

    services.AddDbContext<YomikazeDbContext>(options =>
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        options.UseSqlServer(connectionString, server => server.EnableRetryOnFailure());
    });
    services.AddScoped<DbContext, YomikazeDbContext>();

    services.AddAutoMapper(typeof(MapperConfigs));



}

static void Configure(WebApplication app)
{
    var env = app.Environment;
    var services = app.Services.CreateScope().ServiceProvider;
    var configuration = services.GetRequiredService<IConfiguration>();
    var dbContext = services.GetRequiredService<YomikazeDbContext>();

    dbContext.Database.Migrate();


    if (env.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();


    app.MapControllers();
    app.UsePathBase("/API/V1");

    app.UseCors(x => x
        .AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowed(origin => true) // allow any origin
        .AllowCredentials()); // allow credentials

}



static IEdmModel GetEdmModel()
{
    var builder = new ODataConventionModelBuilder();
    builder.EntitySet<Chapter>("Chapters");
    //builder.EntitySet<Order>("Orders");
    return builder.GetEdmModel();
}