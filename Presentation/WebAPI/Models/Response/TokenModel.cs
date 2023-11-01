using System.IdentityModel.Tokens.Jwt;
using Yomikaze.WebAPI.Helpers;

namespace Yomikaze.WebAPI.Models.Response;

public class TokenModel
{
    public string? Token { get; set; }

    public TokenModel(string? token)
    {
        Token = token;
    }

    public TokenModel(JwtSecurityToken token) : this(token.ToTokenString())
    {
    }
}