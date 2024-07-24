using Yomikaze.Domain.Entities;

namespace Yomikaze.Domain.Models;

public class ReportModel : BaseModel
{
    #region CommonProperties

    public string? Description { get; set; } = default!;

    [SwaggerIgnore]
    [SwaggerSchema(ReadOnly = true)]
    public ReportStatus Status { get; set; } = default!;

    [SwaggerIgnore]
    [SwaggerSchema(ReadOnly = true)]
    public string? DismissalReason { get; set; }

    public string[]? Images { get; set; }

    #endregion

    #region ReadOnlyProperties

    [SwaggerSchema(ReadOnly = true)] public string Reason { get; set; } = default!;

    [SwaggerSchema(ReadOnly = true)] public ProfileModel Reporter { get; set; } = default!;

    #endregion

    #region WriteOnlyProperties

    [SwaggerSchema(WriteOnly = true)]
    [Required]
    public string ReporterId { get; set; } = default!;

    #endregion
}