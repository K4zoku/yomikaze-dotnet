using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Yomikaze.API.Main;
using Yomikaze.Application.Data.Configs;
using Yomikaze.Application.Helpers;
using Yomikaze.Application.Helpers.Security;
using Yomikaze.Infrastructure;
using Yomikaze.Infrastructure.Context;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
IConfiguration configuration = builder.Configuration;

Provider provider = Provider.FromName(configuration.GetValue("provider", Provider.SqlServer.Name));
services.AddDbContext<YomikazeDbContext>(provider, configuration, "Yomikaze");
services.AddScoped<DbContext, YomikazeDbContext>();

services.AddScoped<IRepository<Comic>, ComicRepository>();
services.AddScoped<ComicRepository>();
services.AddScoped<IRepository<Chapter>, ChapterRepository>();
services.AddScoped<ChapterRepository>();
services.AddScoped<IRepository<Tag>, TagRepository>();
services.AddScoped<TagRepository>();
services.AddScoped<IRepository<TagCategory>, TagCategoryRepository>();
services.AddScoped<TagCategoryRepository>();
services.AddScoped<IRepository<HistoryRecord>, HistoryRepository>();
services.AddScoped<HistoryRepository>();
services.AddScoped<IRepository<LibraryEntry>, LibraryRepository>();
services.AddScoped<LibraryRepository>();
services.AddScoped<IRepository<LibraryCategory>, LibraryCategoryRepository>();
services.AddScoped<LibraryCategoryRepository>();

services.AddControllers(options =>
{
    options.Filters.Add<HttpResponseExceptionFilter>();
    options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
}).AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new DefaultContractResolver
    {
        NamingStrategy = new CamelCaseNamingStrategy()
    };
    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    options.SerializerSettings.Converters.Add(new StringEnumConverter());
    JsonConvert.DefaultSettings = () => options.SerializerSettings;
});

services.AddRouting(options =>
{
    options.LowercaseUrls = true;
});

services.AddYomikazeIdentity();

JwtConfiguration jwt = configuration
                           .GetRequiredSection(JwtConfiguration.SectionName)
                           .Get<JwtConfiguration>()
                       ?? throw new InvalidOperationException("Could not read JWT Configuration");
services.AddSingleton(jwt);
services.AddJwtBearerAuthentication(jwt);

services.AddEndpointsApiExplorer();
services.AddSwaggerGenWithJwt();
services.AddSwaggerGenNewtonsoftSupport();
services.AddPublicCors();

services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = configuration.GetSection("Redis").GetConnectionString("Yomikaze");
    options.InstanceName = "Yomikaze";
});

// add auto-mapper
services.AddAutoMapper(typeof(YomikazeMapper));

WebApplication app = builder.Build();
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