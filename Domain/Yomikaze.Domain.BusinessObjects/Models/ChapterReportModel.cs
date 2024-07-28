using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Yomikaze.Domain.Models;

[ValidateNever]
public class ChapterReportModel : ReportModel
{
    #region WriteOnlyProperties

    [SwaggerIgnore] 
    public string ChapterId { get; set; } = default!;
    
    [Required]
    public string ReasonId { get; set; } = default!;

    #endregion
}