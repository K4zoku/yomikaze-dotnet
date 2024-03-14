using Microsoft.OpenApi.Models;

namespace Yomikaze.Domain.Helpers.Security;

public class JwtSecurityScheme : OpenApiSecurityScheme
{
    public JwtSecurityScheme()
    {
        In = ParameterLocation.Header;
        Description = "Please enter token";
        Name = "Authorization";
        Type = SecuritySchemeType.Http;
        BearerFormat = "JWT";
        Scheme = "Bearer";
    }
}