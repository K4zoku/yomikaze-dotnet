using System.Text.Json.Serialization;

namespace Yomikaze.Application.Data.Models.Common;

public class CommentModel
{
    public long Id { get; set; }
    public required string Content { get; set; } = default!;

    public DateTimeOffset CreatedAt { get; set; }
    public required UserModel CreatedBy { get; set; } = default!;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public DateTimeOffset UpdatedAt { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public CommentModel[]? Replies { get; set; } = default!;
}
