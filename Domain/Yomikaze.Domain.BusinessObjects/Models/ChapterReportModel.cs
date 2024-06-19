namespace Yomikaze.Domain.Models;

public class ChapterReportModel : ReportModel
{
    #region ReadOnlyProperties

    [SwaggerSchema(ReadOnly = true)]
    public ChapterModel Chapter { get; set; } = default!;

    #endregion
    
    #region WriteOnlyProperties
    
    [SwaggerSchema(WriteOnly = true)]
    public string ChapterId { get; set; } = default!;
    
    #endregion
}