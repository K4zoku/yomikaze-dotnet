using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Yomikaze.Domain.Models;

[ValidateNever]
public class CommentReportModel : ReportModel
{
    [ValidateNever]
    [SwaggerSchema(ReadOnly = true)]
    public CommentModel Comment { get; set; } = default!;

    #region WriteOnlyProperties

    [SwaggerIgnore] public string CommentId { get; set; } = default!;

    [Required] public string ReasonId { get; set; } = default!;

    #endregion
}