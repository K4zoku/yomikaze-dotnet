using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Yomikaze.Domain.Helpers.Security;

public static class JwtBearerOptions
{
    public static void Configure(Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerOptions options, SecurityKey signingKey)
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            RequireExpirationTime = false,
            IssuerSigningKey = signingKey
        };
    }
}