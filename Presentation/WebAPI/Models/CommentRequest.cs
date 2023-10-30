namespace Yomikaze.WebAPI.Models;

public class CommentRequest : Comment
{
    public long ComicId { get; set; }

    public long? ReplyToId { get; set; }
}
