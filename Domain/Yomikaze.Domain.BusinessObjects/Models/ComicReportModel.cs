namespace Yomikaze.Domain.Models;

public class ComicReportModel
{
    #region ReadOnlyProperties

    [SwaggerSchema(ReadOnly = true)]
    public ComicModel Comic { get; set; } = default!;

    #endregion
    
    #region WriteOnlyProperties
    
    [SwaggerSchema(WriteOnly = true)]
    public string TranslationId { get; set; } = default!;
    
    #endregion
}