using System.IdentityModel.Tokens.Jwt;
using Yomikaze.Application.Helpers;

namespace Yomikaze.Application.Data.Models.Response;

public class TokenModel
{
    public string? Token { get; set; }

    public TokenModel()
    {

    }

    public TokenModel(string? token)
    {
        Token = token;
    }

    public TokenModel(JwtSecurityToken token) : this(token.ToTokenString())
    {
    }
}