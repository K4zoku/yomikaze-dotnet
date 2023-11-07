using Microsoft.AspNetCore.Authentication.JwtBearer;
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
using Yomikaze.Infrastructure.Data;
using Yomikaze.WebAPI.Helpers;
using Yomikaze.WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
var configuration = builder.Configuration;

// Add database
var connectionString = configuration.GetConnectionString("DefaultConnection");
services.AddDbContext<YomikazeDbContext>(options => options.UseSqlServer(connectionString, server => server.EnableRetryOnFailure()));

services.AddIdentity<User, IdentityRole<long>>(options =>
    {
        options.User.RequireUniqueEmail = true;
        options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";

        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
    })
    .AddEntityFrameworkStores<YomikazeDbContext>()
    .AddDefaultTokenProviders();
services.AddCors(options => options.AddPolicy("CorsPolicy",
        builder =>
        {
            builder.AllowAnyHeader()
                   .AllowAnyMethod()
                   .SetIsOriginAllowed((host) => true)
                   .AllowCredentials();
        }));
JwtConfiguration jwt = configuration
    .GetRequiredSection(JwtConfiguration.SectionName)
    .Get<JwtConfiguration>() ?? throw new Exception("Could not read JWT Configuration");

services.AddSingleton(jwt);

services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;

        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = jwt.Issuer,
            ValidAudience = jwt.Audience,
            RequireExpirationTime = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Secret)),
        };
    });
services
    .AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = new Dictionary<string, IEnumerable<string>>();
            foreach (var keyModelStatePair in context.ModelState)
            {
                var key = keyModelStatePair.Key;
                var errorsToAdd = from error in keyModelStatePair.Value.Errors
                                  select string.IsNullOrEmpty(error.ErrorMessage)
                                        ? "The value you entered is invalid"
                                        : error.ErrorMessage;
                errors.Add(key, errorsToAdd.ToArray());
            }
            var problems = ResponseModel.CreateError("Validation errors", errors);
            return new BadRequestObjectResult(problems);
        };
    });
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "YomikazeAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});
services.AddScoped<YomikazeDbInitializer>();

services.AddScoped<IDao<Comic>, ComicDao>();
services.AddScoped<IDao<Comment>, CommentDao>();
services.AddScoped<IDao<Genre>, GenreDao>();

services.AddScoped<AuthenticationService>();
services.AddSingleton<ImageUploadService>();
services.AddScoped<CommentService>();


services.AddSignalR();

var app = builder.Build();
var env = app.Environment;
// Configure the HTTP request pipeline.
if (env.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");
//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<YomikazeHub>("/hubs/yomikaze");
// Initialize database
await using (var scope = app.Services.CreateAsyncScope())
{
    var scopedServices = scope.ServiceProvider;
    var dbInitializer = scopedServices.GetRequiredService<YomikazeDbInitializer>();
    dbInitializer.Initialize();

    // Add default admin user
    var userManager = scopedServices.GetRequiredService<UserManager<User>>();
    var roleManager = scopedServices.GetRequiredService<RoleManager<IdentityRole<long>>>();
    if (!roleManager.Roles.Any(r => r.Name == "Administrator"))
    {
        var role = new IdentityRole<long>("Administrator");
        await roleManager.CreateAsync(role);
    }
    if (!userManager.Users.Any(u => u.UserName == "admin"))
    {
        var user = new User
        {
            UserName = "admin",
            Email = "admin@localhost",
            Fullname = "Administrator"
        };
        await userManager.CreateAsync(user, "Admin@123");
        await userManager.AddToRoleAsync(user, "Administrator");
    }
}

await app.RunAsync();