using System.Text.Json.Serialization;

namespace Yomikaze.Domain.Identity.Models;

public class UserInputModel
{
    public string? Fullname { get; set; }

    public string? Avatar { get; set; }

    public string? Banner { get; set; }

    public string? Bio { get; set; }

    public DateTimeOffset? Birthday { get; set; }
}

public class UserOutputModel
{
    public string Id { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Fullname { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Email { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Username { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Avatar { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Banner { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Bio { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTimeOffset? Birthday { get; set; }
}