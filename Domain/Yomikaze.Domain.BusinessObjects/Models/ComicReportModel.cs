using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Yomikaze.Domain.Models;

[ValidateNever]
public class ComicReportModel : ReportModel
{
    #region ReadOnlyProperties

    [SwaggerSchema(ReadOnly = true)] 
    [ValidateNever]
    public ComicModel Comic { get; set; } = default!;

    #endregion

    #region WriteOnlyProperties

    [SwaggerSchema(ReadOnly = true)]
    [SwaggerIgnore]
    [WriteOnly]
    public string ComicId { get; set; } = default!;

    #endregion
}