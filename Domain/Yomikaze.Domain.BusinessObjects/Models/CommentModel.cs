namespace Yomikaze.Domain.Models;

public class CommentModel : BaseModel
{
    #region CommonProperties

    public string? Content { get; set; } = default!;

    #endregion

    #region ReadOnlyProperties

    [SwaggerSchema(ReadOnly = true)]
    public ProfileModel? Author { get; set; }

    #endregion

    #region WriteOnlyProperties

    [SwaggerIgnore]
    [SwaggerSchema(ReadOnly = true)]
    [WriteOnly]
    public string? AuthorId { get; set; } = default!;

    [SwaggerIgnore]
    [SwaggerSchema(ReadOnly = true)]
    [WriteOnly]
    public string? ReplyToId { get; set; }

    #endregion
}