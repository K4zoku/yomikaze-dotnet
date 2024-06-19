using Yomikaze.Domain.Entities;

namespace Yomikaze.Domain.Models;

public class ReportModel : BaseModel
{
    #region ReadWriteProperties

    public string? Description { get; set; } = default!;

    public ReportStatus Status { get; set; } = default!;

    public string? DismissalReason { get; set; }

    public string[]? Images { get; set; }

    #endregion

    #region ReadOnlyProperties

    [SwaggerSchema(ReadOnly = true)] public string Category { get; set; } = default!;

    [SwaggerSchema(ReadOnly = true)] public ProfileModel Reporter { get; set; } = default!;

    #endregion

    #region WriteOnlyProperties

    [SwaggerSchema(WriteOnly = true)]
    [Required]
    public string CategoryId { get; set; } = default!;

    [SwaggerSchema(WriteOnly = true)]
    [Required]
    public string ReporterId { get; set; } = default!;

    #endregion
}