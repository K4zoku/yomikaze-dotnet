namespace Yomikaze.Domain.Models;

public class TagCategoryModel : BaseModel
{
    #region CommonProperties

    [Required] public string Name { get; set; } = default!;

    #endregion
}