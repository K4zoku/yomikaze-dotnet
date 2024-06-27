namespace Yomikaze.Domain.Models;

public class TranslationModel : BaseModel
{
    #region CommonProperties

    [Required] public int X { get; set; }

    [Required] public int Y { get; set; }

    [Required] public int Width { get; set; }

    [Required] public int Height { get; set; }

    [Required] public string Content { get; set; } = default!;

    [Required] public string Language { get; set; } = default!;

    [Required] public string Alignment { get; set; } = default!;

    #endregion

    #region WriteOnlyProperties

    [SwaggerSchema(WriteOnly = true)] public string? UserId { get; set; }

    [SwaggerSchema(WriteOnly = true)] public string? PageId { get; set; }

    #endregion
}