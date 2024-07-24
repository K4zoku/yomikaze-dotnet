namespace Yomikaze.Domain.Models;

public class ReportReasonModel : BaseModel
{
    #region CommonProperties

    [Required] public string Content { get; set; } = default!;

    #endregion
}