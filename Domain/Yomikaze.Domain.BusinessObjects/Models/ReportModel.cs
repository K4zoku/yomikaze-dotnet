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
    
    [SwaggerSchema(ReadOnly = true)]
    public string Category { get; set; } = default!;
    
    [SwaggerSchema(ReadOnly = true)]
    public ProfileModel Reporter { get; set; } = default!;
    
    #endregion
    
    #region WriteOnlyProperties
    
    [Required] public string CategoryId { get; set; }
    
    [Required] public string ReporterId { get; set; }
    
    #endregion
}