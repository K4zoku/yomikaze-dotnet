namespace Yomikaze.Domain.Models;

public class TagCategoryModel : BaseModel
{
    #region CommonProperties

    [Required] [StringLength(64)] public string Name { get; set; } = default!;

    #endregion
}