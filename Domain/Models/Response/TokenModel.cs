namespace Yomikaze.Domain.Models.Response;

public class TokenModel
{
    public TokenModel()
    {
    }

    public TokenModel(string? token)
    {
        Token = token;
    }

    public string? Token { get; set; }
}