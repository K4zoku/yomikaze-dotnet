using System.Text.Json.Serialization;

namespace Yomikaze.WebAPI.Models;

public class AuthResponse : Response<TokenResponse>
{
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public string? Token { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override TokenResponse? Data => Token == null ? null : new TokenResponse { Token = Token };
}

public class TokenResponse
{
    public string? Token { get; set; }
}