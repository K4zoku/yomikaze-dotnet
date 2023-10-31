namespace Yomikaze.WebAPI.Models;

public class CommentRequest : CommentModel
{
    public long ComicId { get; set; }

    public long? ReplyToId { get; set; }
}
