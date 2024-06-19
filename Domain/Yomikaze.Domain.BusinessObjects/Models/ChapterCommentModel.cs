namespace Yomikaze.Domain.Models;

public class ChapterCommentModel : CommentModel
{
    #region WriteOnlyProperties
    
    public string? ChapterId { get; set; } = default!;
    
    #endregion
}