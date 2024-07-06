namespace Yomikaze.Domain.Models;

public class LibraryCategoryModel : BaseModel
{
    #region CommonProperties

    [Required] public string? Name { get; set; } = default!;

    #endregion

    #region WriteOnlyProperties

    [WriteOnly]
    [SwaggerSchema(ReadOnly = true)]
    public string? UserId { get; set; } = default!;

    #endregion
}