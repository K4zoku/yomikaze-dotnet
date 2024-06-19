namespace Yomikaze.Domain.Models;

public class LibraryCategoryModel : BaseModel
{
    #region ReadWriteProperties

    [Required] public string? Name { get; set; } = default!;

    #endregion

    #region WriteOnlyProperties

    public string? UserId { get; set; } = default!;

    #endregion
}