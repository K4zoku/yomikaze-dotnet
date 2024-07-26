namespace Yomikaze.Domain.Models;

public class ChapterCommentModel : CommentModel
{
    #region WriteOnlyProperties

    [SwaggerIgnore]
    [SwaggerSchema(ReadOnly = true)]
    [WriteOnly]
    public string? ChapterId { get; set; }

    #endregion
}