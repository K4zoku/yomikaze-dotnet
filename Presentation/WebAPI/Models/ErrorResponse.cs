using System.Text.Json.Serialization;

namespace Yomikaze.WebAPI.Models;

public class ErrorResponse : Response
{
    public override bool Success => false;

    [JsonIgnore]
    public override object? Data => null;
    public required object Errors { get; set; }
}