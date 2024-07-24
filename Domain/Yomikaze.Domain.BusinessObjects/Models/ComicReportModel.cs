namespace Yomikaze.Domain.Models;

public class ComicReportModel : ReportModel
{
    #region ReadOnlyProperties

    [SwaggerSchema(ReadOnly = true)] public ComicModel Comic { get; set; } = default!;

    #endregion

    #region WriteOnlyProperties

    [SwaggerSchema(ReadOnly = true)]
    [SwaggerIgnore]
    [WriteOnly]
    public string ComicId { get; set; } = default!;
    
    [WriteOnly]
    public string ReasonId { get; set; } = default!;

    #endregion
}