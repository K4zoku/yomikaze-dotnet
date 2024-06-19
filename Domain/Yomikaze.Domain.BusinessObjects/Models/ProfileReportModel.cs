namespace Yomikaze.Domain.Models;

public class ProfileReportModel : ReportModel
{
    #region ReadOnlyProperties

    [SwaggerSchema(ReadOnly = true)]
    public ProfileModel Profile { get; set; } = default!;

    #endregion
    
    #region WriteOnlyProperties
    
    [SwaggerSchema(WriteOnly = true)]
    public string ProfileId { get; set; } = default!;
    
    #endregion
}