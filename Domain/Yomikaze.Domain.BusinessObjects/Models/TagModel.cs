using Swashbuckle.AspNetCore.Annotations;

namespace Yomikaze.Domain.Models;

public class TagModel : BaseModel
{
    #region ReadWriteProperties

    [Required]
    [Length(1, 50, ErrorMessage = "Genre's name must be between 1 and 100 characters")]
    public string? Name { get; set; } = default!;
    
    public string? Description { get; set; }

    #endregion

    #region ReadOnlyProperties

    [SwaggerSchema(ReadOnly = true)]
    public string Category { get; set; } = default!;

    #endregion

    #region WriteOnlyProperties

    [SwaggerSchema(WriteOnly = true)]
    [Required]
    public string? CategoryId { get; set; } = default!;

    #endregion
}