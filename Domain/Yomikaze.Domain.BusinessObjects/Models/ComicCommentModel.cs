namespace Yomikaze.Domain.Models;

public class ComicCommentModel : CommentModel
{
    #region WriteOnlyProperties

    [SwaggerIgnore]
    [SwaggerSchema(ReadOnly = true)]
    [WriteOnly]
    public string? ComicId { get; set; } = default!;

    #endregion
}