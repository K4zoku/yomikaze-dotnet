namespace Yomikaze.Domain.Entities;

public class ChapterCommentReport : CommentReport
{
    public ChapterComment Comment { get; set; } = default!;
}