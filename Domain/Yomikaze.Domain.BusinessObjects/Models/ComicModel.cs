using Yomikaze.Domain.Entities;

namespace Yomikaze.Domain.Models;

public class ComicModel : BaseModel
{
    [Required]
    [Length(0, 150, ErrorMessage = "Comic's name must from 0 to 150 characters")]
    public string? Name { get; set; } = default!;
    
    public string[]? Aliases { get; set; } = [];

    [Length(0, 500, ErrorMessage = "Comic's description must from 0 to 500 characters")]
    public string? Description { get; set; }

    public string? Cover { get; set; }

    public string? Banner { get; set; }

    public DateTimeOffset? PublicationDate { get; set; }

    [SwaggerSchema(ReadOnly = true)] public TagModel[] Tags { get; set; } = [];

    [SwaggerSchema(WriteOnly = true)] public string[] TagIds { get; set; } = [];
    
    public string[]? Authors { get; set; } = [];

    [SwaggerSchema(ReadOnly = true)] 
    public ProfileModel? Publisher { get; set; } = default!;

    [Required]
    [SwaggerSchema(WriteOnly = true)]
    public string? PublisherId { get; set; }

    public ComicStatus? Status { get; set; }
}