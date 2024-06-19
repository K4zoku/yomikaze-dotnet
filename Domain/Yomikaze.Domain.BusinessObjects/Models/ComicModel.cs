using Yomikaze.Domain.Entities;

namespace Yomikaze.Domain.Models;

public class ComicModel : BaseModel
{
    [Required]
    [Length(0, 150, ErrorMessage = "Comic's name must from 0 to 150 characters")]
    public string? Name { get; set; } = default!;

    [StringLength(150, ErrorMessage = "Comic's aliases must from 0 to 150 characters")]
    public IList<string>? Aliases { get; set; } = new List<string>();

    [Length(0, 500, ErrorMessage = "Comic's description must from 0 to 500 characters")]
    public string? Description { get; set; }

    public string? Cover { get; set; }

    public string? Banner { get; set; }

    public DateTimeOffset? PublicationDate { get; set; }

    [SwaggerSchema(ReadOnly = true)] public ICollection<TagModel> Tags { get; set; } = new List<TagModel>();

    [SwaggerSchema(WriteOnly = true)] public ICollection<string>? TagIds { get; set; } = new List<string>();

    [StringLength(70, ErrorMessage = "Comic's authors must from 0 to 70 characters")]
    public ICollection<string>? Authors { get; set; } = new List<string>();

    [SwaggerSchema(ReadOnly = true)] public ProfileModel Publisher { get; set; } = default!;

    [SwaggerSchema(WriteOnly = true)]
    [Required]
    public string? PublisherId { get; set; }

    [SwaggerSchema(ReadOnly = true)] public ICollection<ChapterModel> Chapters { get; set; } = new List<ChapterModel>();

    [SwaggerSchema(WriteOnly = true)] public ICollection<string>? ChapterIds { get; set; } = new List<string>();

    public ComicStatus? Status { get; set; }
}