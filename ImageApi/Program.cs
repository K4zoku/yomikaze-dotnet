
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Yomikaze.Application.Data.Access;
using Yomikaze.Application.Data.Hubs;
using Yomikaze.Application.Data.Models.Response;
using Yomikaze.Domain.Common;
using Yomikaze.Domain.Database.Entities;
using Yomikaze.Domain.Database.Entities.Identity;
using Yomikaze.ImageApi.Services;
using Yomikaze.Infrastructure.Data;


var builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
var configuration = builder.Configuration;



services.AddControllers();


services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddSingleton<ImageUploadService>();


var app = builder.Build();
var env = app.Environment;
// Configure the HTTP request pipeline.
if (env.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Initialize database


await app.RunAsync();