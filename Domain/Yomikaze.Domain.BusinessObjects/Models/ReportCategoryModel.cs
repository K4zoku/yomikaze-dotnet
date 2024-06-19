namespace Yomikaze.Domain.Models;

public class ReportCategoryModel : BaseModel
{
    #region ReadWriteProperties

    [Required] public string Name { get; set; } = default!;

    public bool RequiresDescription { get; set; } = false;

    #endregion
}