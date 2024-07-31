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

    #endregion

    #region ReadOnlyProperties
    
    [Required]
    public string ReasonId { get; set; } = default!;

    [SwaggerSchema(ReadOnly = true)] public ReportReasonModel Reason { get; set; } = default!;

    [SwaggerSchema(ReadOnly = true)] public ProfileModel Reporter { get; set; } = default!;

    #endregion

    #region WriteOnlyProperties

    [SwaggerIgnore]
    [SwaggerSchema(ReadOnly = true)]
    [WriteOnly]
    public string ReporterId { get; set; } = default!;

    #endregion
}