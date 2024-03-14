using System.IdentityModel.Tokens.Jwt;
using Yomikaze.Domain.Helpers;
using Yomikaze.Domain.Helpers.Security;

namespace Yomikaze.Application.Data.Models.Response;

public class TokenModel
{
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

    public string? Token { get; set; }
}