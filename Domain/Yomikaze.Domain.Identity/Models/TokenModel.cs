namespace Yomikaze.Domain.Identity.Models;

public class TokenModel(string? token)
{
    public string? Token { get; set; } = token;
}