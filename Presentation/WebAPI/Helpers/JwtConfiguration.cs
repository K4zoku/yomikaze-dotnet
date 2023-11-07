using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Yomikaze.WebAPI.Helpers;

public class JwtConfiguration
{
    public const string SectionName = "JWT";
    public string Audience { get; set; } = default!;
    public string Issuer { get; set; } = default!;
    public string Secret { get; set; } = default!;

    public SecurityKey SecurityKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));
    public SigningCredentials SigningCredentials => new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
}
