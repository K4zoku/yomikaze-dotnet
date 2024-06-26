using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace Yomikaze.Domain.Models;

public class BaseModel
{
    [SwaggerSchema(ReadOnly = true)]
    public string Id { get; set; } = default!;
    
    [SwaggerSchema(ReadOnly = true)]
    [JsonInclude]
    public DateTimeOffset? CreationTime { get; set; }
    
    [SwaggerSchema(ReadOnly = true)]
    public DateTimeOffset? LastModification { get; set; }
}