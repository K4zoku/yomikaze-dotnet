namespace Yomikaze.Domain.Models;

public class ReportCategoryModel : BaseModel
{
    #region CommonProperties

    [Required] public string Name { get; set; } = default!;

    public bool RequiresDescription { get; set; } = false;

    #endregion
}