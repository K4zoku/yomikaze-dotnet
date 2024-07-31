namespace Yomikaze.Domain.Entities;

public class ChapterCommentReport : CommentReport
{
    public new ChapterComment Comment { get; set; } = default!;
}