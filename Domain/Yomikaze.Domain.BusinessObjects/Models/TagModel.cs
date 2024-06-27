namespace Yomikaze.Domain.Models;

public class TagModel : BaseModel
{
    #region ReadOnlyProperties

    [SwaggerSchema(ReadOnly = true)] public string Category { get; set; } = default!;

    #endregion

    #region WriteOnlyProperties

    [SwaggerSchema(WriteOnly = true)]
    [Required]
    public string? CategoryId { get; set; }

    #endregion

    #region CommonProperties

    [Required]
    [Length(1, 50, ErrorMessage = "Genre's name must be between 1 and 100 characters")]
    public string? Name { get; set; }

    public string? Description { get; set; }

    #endregion
}