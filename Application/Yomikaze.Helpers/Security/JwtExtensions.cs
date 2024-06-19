using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Yomikaze.Application.Helpers.Security;

public static class JwtExtensions
{
    public static string ToTokenString(this JwtSecurityToken token)
    {
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public static IServiceCollection AddJwtBearerAuthentication(this IServiceCollection services,
        JwtConfiguration configuration)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = configuration.SaveToken;
                options.RequireHttpsMetadata = configuration.RequireHttpsMetadata;
                options.Audience = configuration.Audience;
                options.ClaimsIssuer = configuration.Issuer;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = configuration.ValidateIssuer,
                    ValidateAudience = configuration.ValidateAudience,
                    RequireExpirationTime = configuration.Expire,
                    IssuerSigningKey = configuration.SecurityKey,
                    RoleClaimType = "roles",
                    NameClaimType = JwtRegisteredClaimNames.Name
                };
                options.MapInboundClaims = false;
            });
        return services;
    }

    public static IServiceCollection AddSwaggerGenWithJwt(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.EnableAnnotations();
            options.AddSecurityDefinition("JWT",
                new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "JWT" }
                    },
                    Array.Empty<string>()
                }
            });
        });
        return services;
    }
}