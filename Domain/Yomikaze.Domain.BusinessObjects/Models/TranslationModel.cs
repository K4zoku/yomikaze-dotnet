namespace Yomikaze.Domain.Models;

public class TranslationModel
{
    #region ReadWriteProperties

    [Required]
    public int X { get; set; }
    
    [Required]
    public int Y { get; set; }
    
    [Required]
    public int Width { get; set; }
    
    [Required]
    public int Height { get; set; }
    
    [Required]
    public string Content { get; set; }
    
    [Required]
    public string Language { get; set; }
    
    [Required]
    public string Alignment { get; set; }

    #endregion
    
    #region WriteOnlyProperties
    
    [SwaggerSchema(WriteOnly = true)]
    public string? UserId { get; set; }

    [SwaggerSchema(WriteOnly = true)]
    public string? PageId { get; set; }
    
    #endregion
}