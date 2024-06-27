namespace Yomikaze.Domain.Models;

public class PageModel : BaseModel
{
    #region WriteOnlyProperties

    [Required]
    [SwaggerSchema(WriteOnly = true)]
    public string? ChapterId { get; set; }

    #endregion

    #region CommonProperties

    [Required] public int? Number { get; set; }

    [Required] public string? Image { get; set; } = default!;

    #endregion
}