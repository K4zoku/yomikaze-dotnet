using System.ComponentModel.DataAnnotations;

namespace Yomikaze.Domain.Models;

public class CommentInputModel
{
    [Required]
    [Length(0, 250, ErrorMessage = "Content must from 0 to 250 characters")]
    public string Content { get; set; } = default!;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

    public long ReplyToId { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }
}

public class CommentOutputModel
{
    public long Id { get; set; }

    public required string Content { get; set; } = default!;

    public DateTimeOffset UpdatedAt { get; set; }

    public CommentOutputModel[]? Replies { get; set; } = default!;
}