using System.Text.Json.Serialization;

namespace Yomikaze.WebAPI.Models.Common;

public class CommentModel
{
    public required string Content { get; set; } = default!;

    public DateTimeOffset CreatedAt { get; set; }
    public required UserModel CreatedBy { get; set; } = default!;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTimeOffset UpdatedAt { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public CommentModel[]? Replies { get; set; } = default!;
}
