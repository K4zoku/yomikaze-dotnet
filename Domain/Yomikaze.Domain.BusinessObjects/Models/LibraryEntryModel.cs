namespace Yomikaze.Domain.Models;

public class LibraryEntryModel : BaseModel
{
    #region ReadOnlyProperties

    public ComicModel? Comic { get; set; }

    public string? Category { get; set; }

    #endregion

    #region WriteOnlyProperties

    [Required] public string? UserId { get; set; }

    [Required] public string? ComicId { get; set; }

    public string? CategoryId { get; set; }

    #endregion
}