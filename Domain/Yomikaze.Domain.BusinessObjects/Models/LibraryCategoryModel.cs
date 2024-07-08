namespace Yomikaze.Domain.Models;

public class LibraryCategoryModel : BaseModel
{
    #region CommonProperties

    [Required]
    [StringLength(32, ErrorMessage = "Category name must be from 1 to 32 characters")]
    public string? Name { get; set; } = default!;

    #endregion

    #region WriteOnlyProperties

    [WriteOnly]
    [SwaggerSchema(ReadOnly = true)]
    public string? UserId { get; set; } = default!;

    #endregion
}