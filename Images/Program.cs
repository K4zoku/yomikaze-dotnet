using Microsoft.Extensions.FileProviders;
using Yomikaze.Domain.Helpers.API;
using Yomikaze.Domain.Helpers.Security;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;

services.AddControllers().ConfigureApiBehaviorOptionsYomikaze();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddPublicCors();

string storagePath = Path.Combine(Directory.GetCurrentDirectory(), "Storage", "Images");
if (!Directory.Exists(storagePath))
{
    Directory.CreateDirectory(storagePath);
}

PhysicalFileProvider fileProvider = new(storagePath);
services.AddSingleton(fileProvider);

WebApplication app = builder.Build();
IWebHostEnvironment env = app.Environment;
// Configure the HTTP request pipeline.
if (env.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = fileProvider, RequestPath = new PathString("/Images"), ServeUnknownFileTypes = false
});

app.UseCors("Public");

app.MapControllers();

await app.RunAsync();