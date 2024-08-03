using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Yomikaze.Domain.Models;

[ValidateNever]
public class ChapterReportModel : ReportModel
{
    #region WriteOnlyProperties

    [SwaggerIgnore] public string ChapterId { get; set; } = default!;

    #endregion

    [ValidateNever]
    [SwaggerSchema(ReadOnly = true)]
    public ChapterModel Chapter { get; set; } = default!;

    [ValidateNever]
    [SwaggerSchema(ReadOnly = true)]
    public ComicModel Comic { get; set; } = default!;
}