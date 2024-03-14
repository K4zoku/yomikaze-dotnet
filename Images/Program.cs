using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.FileProviders;
using Yomikaze.Application.Data.Models.Response;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;

services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            Dictionary<string, IEnumerable<string>> errors = new();
            foreach ((string? key, ModelStateEntry? value) in context.ModelState)
            {
                IEnumerable<string> errorsToAdd = value.Errors.Where(error => !string.IsNullOrEmpty(error.ErrorMessage))
                    .Select(error => error.ErrorMessage);
                errors.Add(key, errorsToAdd);
            }

            ResponseModel<object, Dictionary<string, IEnumerable<string>>> problems =
                ResponseModel.CreateError("Validation errors", errors);
            return new BadRequestObjectResult(problems);
        };
    });
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddCors(options =>
{
    options.AddPolicy("Public", cors =>
    {
        cors.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
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