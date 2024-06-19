using System.Text.Json.Serialization;

namespace Yomikaze.Domain.Models;

public class BaseModel
{
    [SwaggerSchema(ReadOnly = true)] public string? Id { get; set; }

    [SwaggerSchema(ReadOnly = true)]
    [JsonInclude]
    public DateTimeOffset? CreationTime { get; set; }

    [SwaggerSchema(ReadOnly = true)] public DateTimeOffset? LastModification { get; set; }
}