using Swashbuckle.AspNetCore.Annotations;

namespace Yomikaze.Domain.Models;

public class PageModel
{
    #region ReadWriteProperties

    [Required]
    public int? Number { get; set; }

    [Required]
    public string? Image { get; set; } = default!;

    #endregion
    
    #region WriteOnlyProperties
    
    [Required]
    [SwaggerSchema(WriteOnly = true)]
    public string? ChapterId { get; set; }
    
    #endregion
}