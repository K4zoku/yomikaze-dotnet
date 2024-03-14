using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Yomikaze.Application.Data.Models.Response;
using Yomikaze.Domain.Entities.Identity;
using Yomikaze.Domain.Helpers;
using Yomikaze.Domain.Helpers.Security;
using Yomikaze.Infrastructure.Database;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
ConfigurationManager configuration = builder.Configuration;

// Database context
services.AddDbContext<YomikazeDbContext>(options =>
{
    string? connectionString = configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString, server => server.EnableRetryOnFailure());
});
services.AddScoped<DbContext, YomikazeDbContext>();

// CORS
services.AddCors(options =>
    options.AddPolicy("PublicCORS",
        cors => cors
            .AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed(_ => true)
            .AllowCredentials()
    )
);

// Identity and JWT
services.AddIdentity<User, Role>(options =>
    {
        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<YomikazeDbContext>()
    .AddDefaultTokenProviders();

JwtConfiguration jwt = configuration
                           .GetRequiredSection(JwtConfiguration.SectionName)
                           .Get<JwtConfiguration>()
                       ?? throw new InvalidOperationException("Could not read JWT Configuration");

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
        options.SaveToken = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            RequireExpirationTime = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Secret))
        };
    });

services
    .AddControllers(options =>
    {
        options.Filters.Add<HttpResponseExceptionFilter>();
    })
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = InvalidModelStateResponse.Factory;
    });

services.AddEndpointsApiExplorer();
services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Authentication", Version = "v1" });
    opt.AddSecurityDefinition("JWT", new JwtSecurityScheme());
    opt.AddSecurityRequirement(new JwtSecurityRequirement());
});

WebApplication app = builder.Build();
IWebHostEnvironment env = app.Environment;

if (env.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("PublicCORS");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

IServiceScope scope = app.Services.CreateScope();
IServiceProvider service = scope.ServiceProvider;
DbContext dbContext = service.GetRequiredService<DbContext>();
dbContext.Database.Migrate();

await app.RunAsync();