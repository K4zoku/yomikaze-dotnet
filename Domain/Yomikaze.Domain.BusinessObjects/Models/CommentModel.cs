namespace Yomikaze.Domain.Models;

public class CommentInputModel
{
    public string ComicId { get; set; }

    [Required]
    [Length(0, 250, ErrorMessage = "Content must from 0 to 250 characters")]
    public string Content { get; set; } = default!;

    public string? ReplyToId { get; set; }
}

public class CommentOutputModel
{
    public ulong Id { get; set; }
    
    public string IdStr { get; set; }

    public string Content { get; set; } = default!;

    public DateTimeOffset LastUpdated { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public IEnumerable<CommentOutputModel> Replies { get; set; } = new List<CommentOutputModel>();
}