namespace Yomikaze.Domain.Models;

public abstract class ChapterModel : BaseModel
{
    #region ReadOnlyProperties

    public ComicModel? Comic { get; set; }

    #endregion

    #region WriteOnlyProperties

    [Required] public string? ComicId { get; set; } = default!;

    #endregion

    #region ReadWriteProperties

    [Required] public int? Number { get; set; }

    [Required]
    [Length(0, 50, ErrorMessage = "Chapter name must from 0 to 50 characters")]
    public string? Name { get; set; } = default!;

    [Length(0, 250, ErrorMessage = "Description must from 0 to 250 characters")]
    public string? Description { get; set; }

    public IList<string>? Pages { get; set; } = new List<string>();

    #endregion
}