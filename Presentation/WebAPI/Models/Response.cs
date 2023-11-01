using System.Text.Json.Serialization;

namespace Yomikaze.WebAPI.Models;

public class Response<T, E>
{
    public virtual bool Success { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual string? Message { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual T? Data { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public E Errors { get; set; } = default!;
}

public class Response : Response<object, IDictionary<string, string>>
{

}

public class Response<T> : Response<T, IDictionary<string, string>>
{

}