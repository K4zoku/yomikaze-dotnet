using Yomikaze.Domain.Entities;

namespace Yomikaze.Domain.Models;

public class ComicModel : BaseModel
{
    #region ReadWriteProperties

    [Required]
    [StringLength(150, ErrorMessage = "Comic's name must from 0 to 150 characters")]
    public string? Name { get; set; }

    public ICollection<string>? Aliases { get; set; } = [];

    [StringLength(500, ErrorMessage = "Comic's description must from 0 to 500 characters")]
    public string? Description { get; set; }

    [SwaggerSchema(Format = "uri")] public string? Cover { get; set; }

    [SwaggerSchema(Format = "uri")] public string? Banner { get; set; }

    public DateTimeOffset? PublicationDate { get; set; }

    public ICollection<string>? Authors { get; set; } = [];

    public ComicStatus? Status { get; set; }

    #endregion

    #region ReadOnlyProperties

    [SwaggerSchema(ReadOnly = true)] public ICollection<TagModel> Tags { get; set; } = [];

    [SwaggerSchema(ReadOnly = true)] public ProfileModel? Publisher { get; set; } = default!;

    #endregion

    #region WriteOnlyProperties

    [Required]
    [SwaggerSchema(WriteOnly = true)]
    public ICollection<string>? TagIds { get; set; } = [];

    [SwaggerSchema(WriteOnly = true)] public string? PublisherId { get; set; }

    #endregion

    #region ReadOnlyExtraProperties

    [SwaggerSchema(ReadOnly = true)] public int? TotalChapters { get; set; }

    [SwaggerSchema(ReadOnly = true)] public int? TotalViews { get; set; }

    [SwaggerSchema(ReadOnly = true)] public double AverageRating { get; set; }

    [SwaggerSchema(ReadOnly = true)] public int? TotalRatings { get; set; }

    #endregion
}