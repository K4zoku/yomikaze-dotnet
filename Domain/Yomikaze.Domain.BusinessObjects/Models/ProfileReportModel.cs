using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Yomikaze.Domain.Models;

[ValidateNever]
public class ProfileReportModel : ReportModel
{
    #region WriteOnlyProperties

    [SwaggerIgnore] public string ProfileId { get; set; } = default!;

    #endregion

    #region ReadOnlyProperties

    [ValidateNever]
    [SwaggerSchema(ReadOnly = true)]
    public ProfileModel Profile { get; set; } = default!;

    #endregion
}