using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Yomikaze.Domain.Database.Entities.Identity;
using Yomikaze.Infrastructure.Data;
using Yomikaze.WebAPI.Helpers;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

// Add database
var connectionString = configuration.GetConnectionString("DefaultConnection");
services.AddDbContext<YomikazeDbContext>(options => options.UseSqlServer(connectionString, server => server.EnableRetryOnFailure()));

services.AddIdentity<YomikazeUser, YomikazeRole>(options =>
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Secret))
        };
    });

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();
var env = app.Environment;
// Configure the HTTP request pipeline.
if (env.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Initialize database
await using (var scope = app.Services.CreateAsyncScope())
{
    var scopedServices = scope.ServiceProvider;
    var dbContext = scopedServices.GetRequiredService<YomikazeDbContext>();
    var dbInitializer = new YomikazeDbInitializer(dbContext);
    await dbInitializer.InitializeAsync();

    // Add default admin user
    var userManager = scopedServices.GetRequiredService<UserManager<YomikazeUser>>();
    var user = await userManager.FindByNameAsync("admin");
    if (user == null)
    {
        user = new YomikazeUser
        {
            UserName = "admin",
            Email = "admin@localhost",
            Fullname = "Administrator"
        };
        await userManager.CreateAsync(user, "Admin@123");
    }
}

await app.RunAsync();