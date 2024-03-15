using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Yomikaze.Application.Data.Models.Common;

public class ComicInputModel
{
    

    [Required, Length(0, 150, ErrorMessage = "Comic's name must from 0 to 150 characters")]
    public string Name { get; set; } = default!;

    [Length(0, 500, ErrorMessage = "Comic's description must from 0 to 500 characters")]
    public string? Description { get; set; }

    public string? Cover { get; set; }

    public string? Banner { get; set; }

    public DateTimeOffset? Published { get; set; }

    public DateTimeOffset? Ended { get; set; }

    [Length(0, 150, ErrorMessage = "Comic's aliases must from 0 to 150 characters")]
    public string? Aliases { get; set; }

    [Length(0, 70, ErrorMessage = "Comic's authors must from 0 to 150 characters")]
    public string? Authors { get; set; }

    //list of genre ids
    public ICollection<long> GenresId { get; set; } = new List<long>();
}

public class ComicOutputModel
{
    public long Id { get; set; }

    public string Name { get; set; } = default!;
    
    public string? Description { get; set; }

    public string? Cover { get; set; }

    public string? Banner { get; set; }

    public DateTimeOffset? Published { get; set; }

    public DateTimeOffset? Ended { get; set; }

    public string? Aliases { get; set; }

    public string? Authors { get; set; }

    public ICollection<GenreOutputModel>? Genres { get; set; }
}
