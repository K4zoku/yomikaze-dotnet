using System.ComponentModel.DataAnnotations;

namespace Yomikaze.Application.Data.Models.Request;

public class CommentRequestModel
{
    public long? Id { get; set; }

    public long ComicId { get; set; }

    public long? ReplyToId { get; set; }

    [Required] public required string Content { get; set; } = default!;
}