using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Yomikaze.Domain.Helpers.Security;

public class JwtConfiguration
{
    public const string SectionName = "JWT";
    public string Secret { get; set; } = default!;
    
    public string? Issuer { get; set; }
    
    public string? Audience { get; set; }
    
    public bool SaveToken { get; set; } = false;
    
    public bool RequireHttpsMetadata { get; set; } = false;
    
    public bool ValidateIssuer => Issuer is not null;
    
    public bool ValidateAudience => Audience is not null;
    
    public bool Expire { get; set; } = false;
    
    public int ExpireMinutes { get; set; } = 30;

    public SecurityKey SecurityKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));
    public SigningCredentials SigningCredentials => new(SecurityKey, SecurityAlgorithms.HmacSha256);
}