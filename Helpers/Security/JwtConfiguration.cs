using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Yomikaze.Domain.Helpers.Security;

public class JwtConfiguration
{
    public const string SectionName = "JWT";
    public string Secret { get; set; } = default!;

    public SecurityKey SecurityKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));
    public SigningCredentials SigningCredentials => new(SecurityKey, SecurityAlgorithms.HmacSha256);
}