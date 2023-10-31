using System.Text.Json.Serialization;

namespace Yomikaze.WebAPI.Models;

public class SignInResponse : Response
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Token { get; set; }
}