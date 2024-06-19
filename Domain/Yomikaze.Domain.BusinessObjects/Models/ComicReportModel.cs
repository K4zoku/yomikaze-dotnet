namespace Yomikaze.Domain.Models;

public class ComicReportModel
{
    #region ReadOnlyProperties

    [SwaggerSchema(ReadOnly = true)]
    public ComicModel Comic { get; set; }

    #endregion
    
    #region WriteOnlyProperties
    
    [SwaggerSchema(WriteOnly = true)]
    public string TranslationId { get; set; }
    
    #endregion
}