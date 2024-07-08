namespace Yomikaze.Domain.Models;

public class LibraryEntryModel : BaseModel
{
    #region ReadOnlyProperties

    [SwaggerSchema(ReadOnly = true)]
    public ComicModel? Comic { get; set; }

    [SwaggerSchema(ReadOnly = true)]
    public string[]? Categories { get; set; }

    #endregion

    #region WriteOnlyProperties

    [WriteOnly]
    public string? UserId { get; set; }

    [WriteOnly]
    public string? ComicId { get; set; }

    [WriteOnly]
    public IList<string>? CategoryIds { get; set; }

    #endregion
}