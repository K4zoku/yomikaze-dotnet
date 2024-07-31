namespace Yomikaze.Domain.Entities;

public class ComicCommentReport : CommentReport
{
    public new ComicComment Comment { get; set; } = default!;
}