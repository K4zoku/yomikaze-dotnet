using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Yomikaze.API.CDN.Images.Configurations;
using Yomikaze.Application.Helpers;
using Yomikaze.Application.Helpers.Security;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;

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
services.AddEndpointsApiExplorer();
services.AddSwaggerGenWithJwt();
services.AddSwaggerGenNewtonsoftSupport();
services.AddPublicCors();   
JwtConfiguration jwt = builder.Configuration
                           .GetRequiredSection(JwtConfiguration.SectionName)
                           .Get<JwtConfiguration>()
                       ?? throw new InvalidOperationException("Could not read JWT Configuration");
services.AddSingleton(jwt);
services.AddJwtBearerAuthentication(jwt);

string storagePath = builder.Configuration["StoragePath"] ?? Path.Combine(Directory.GetCurrentDirectory(), "Storage", "Images");
if (!Directory.Exists(storagePath))
{
    Directory.CreateDirectory(storagePath);
}

PhysicalFileProvider fileProvider = new(storagePath);

services.AddSingleton(fileProvider);

services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost;
});


WebApplication app = builder.Build();
IWebHostEnvironment env = app.Environment;
app.UseForwardedHeaders();
if (env.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
var defaultFile = fileProvider.GetFileInfo("default.webp");
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = fileProvider, RequestPath = new PathString("/images"), ServeUnknownFileTypes = false, 
    OnPrepareResponse = context =>
    {
        var httpContext = context.Context;
        var response = httpContext.Response;
        if (response.StatusCode is not 404)
        {
            return;
        }
        var body = response.Body;
        defaultFile.CreateReadStream().CopyTo(body);
        body.Close();
        response.Headers.ContentType = "image/webp";
    }
});

app.UseCors("Public");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();