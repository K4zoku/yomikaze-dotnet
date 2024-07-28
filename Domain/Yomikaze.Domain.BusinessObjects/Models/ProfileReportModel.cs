using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Yomikaze.Domain.Models;

[ValidateNever]
public class ProfileReportModel : ReportModel
{

    #region WriteOnlyProperties
    
    [SwaggerIgnore]
    public string ProfileId { get; set; } = default!;
    
    [Required] public ulong ReasonId { get; set; }

    #endregion
}