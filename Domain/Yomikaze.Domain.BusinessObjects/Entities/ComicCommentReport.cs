namespace Yomikaze.Domain.Entities;

public class ComicCommentReport : CommentReport
{
    public ComicComment Comment { get; set; } = default!;
}