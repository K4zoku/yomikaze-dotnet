using System.Text.Json.Serialization;

namespace Yomikaze.Application.Data.Models.Common;

public class ComicInputModel
{
    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    public string? Cover { get; set; }

    public string? Banner { get; set; }

    public DateTimeOffset? Published { get; set; }

    public DateTimeOffset? Ended { get; set; } 

    public string? Aliases { get; set; }

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
