using System.Text.Json.Serialization;

namespace Yomikaze.WebAPI.Models;

public class Response
{
    public virtual bool Success { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual string? Message { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual object? Data { get; set; }
}