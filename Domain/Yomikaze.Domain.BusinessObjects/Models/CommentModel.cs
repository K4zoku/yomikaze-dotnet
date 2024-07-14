namespace Yomikaze.Domain.Models;

public class CommentModel : BaseModel
{
    #region CommonProperties

    public string? Content { get; set; } = default!;

    #endregion

    #region ReadOnlyProperties

    [SwaggerSchema(ReadOnly = true)]
    public ProfileModel? Author { get; set; }

    [SwaggerSchema(ReadOnly = true)]
    public ICollection<CommentModel>? Replies { get; set; }

    #endregion

    #region WriteOnlyProperties

    [SwaggerSchema(ReadOnly = true)]
    [WriteOnly]
    public string? AuthorId { get; set; } = default!;

    [SwaggerSchema(WriteOnly = true)]
    [WriteOnly]
    public string? ReplyToId { get; set; }

    #endregion
}