namespace Yomikaze.Domain.Models;

public class ComicCommentModel : CommentModel
{
    #region WriteOnlyProperties

    public string? ComicId { get; set; } = default!;

    #endregion
}