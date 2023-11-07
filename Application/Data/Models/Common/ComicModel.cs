using System.Text.Json.Serialization;
using Yomikaze.Domain.Database.Entities;

namespace Yomikaze.Application.Data.Models.Common;

public class ComicModel
{
    public long Id { get; set; }
    public required string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? Cover { get; set; }
    public string? Banner { get; set; }
    public DateTimeOffset? Published { get; set; }
    public DateTimeOffset? Ended { get; set; }
    public virtual string? Aliases { get; set; } = default!;
    public virtual string? Authors { get; set; } = default!;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual ICollection<GenreModel>? Genres { get; set; } = default!;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual ICollection<ChapterModel>? Chapters { get; set; } = default!;

    public static explicit operator ComicModel(Comic comic)
    {
        return new()
        {
            Id = comic.Id,
            Name = comic.Name,
            Description = comic.Description,
            Cover = comic.Cover,
            Banner = comic.Banner,
            Published = comic.Published,
            Ended = comic.Ended,
            Aliases = comic.Aliases,
            Authors = comic.Authors,
            Genres = comic.Genres.Select(g => g.ToModel()).ToList(),
            Chapters = comic.Chapters.Select(c => c.ToModel()).ToList()
        };
    }

}
