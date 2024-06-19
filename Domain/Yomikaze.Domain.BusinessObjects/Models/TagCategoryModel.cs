namespace Yomikaze.Domain.Models;

public class TagCategoryModel : BaseModel
{
    #region ReadWriteProperties

    [Required]
    public string Name { get; set; } = default!;

    #endregion
}