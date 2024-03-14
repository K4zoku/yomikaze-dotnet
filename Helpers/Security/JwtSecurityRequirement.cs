using Microsoft.OpenApi.Models;

namespace Yomikaze.Domain.Helpers.Security;

public class JwtSecurityRequirement : OpenApiSecurityRequirement
{
    public JwtSecurityRequirement(string id = "JWT")
    {
        Add(new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = id
            }
        }, 
        Array.Empty<string>());
    }
}