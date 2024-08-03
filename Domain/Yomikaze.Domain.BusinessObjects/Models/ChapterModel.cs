namespace Yomikaze.Domain.Models;

public class ChapterModel : BaseModel
{
    #region WriteOnlyProperties

    [WriteOnly] public string? ComicId { get; set; }

    #endregion

    #region ReadOnlyProperties

    [SwaggerSchema(ReadOnly = true)] public int? Views { get; set; }

    [SwaggerSchema(ReadOnly = true)] public bool? HasLock { get; set; }

    #endregion

    #region CommonProperties

    [Required] public int? Number { get; set; }

    [Required]
    [Length(0, 50, ErrorMessage = "Chapter name must from 0 to 50 characters")]
    public string? Name { get; set; }

    [MinLength(1, ErrorMessage = "Chapter must have at least 1 page")]
    public IList<string>? Pages { get; set; }

    public int? Price { get; set; }

    #endregion

    #region Extras

    [SwaggerSchema(ReadOnly = true)] public bool? IsUnlocked { get; set; }

    [SwaggerSchema(ReadOnly = true)] public int? TotalComments { get; set; }

    public bool? IsRead { get; set; }

    #endregion
}