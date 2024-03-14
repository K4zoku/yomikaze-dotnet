using System.Text.Json.Serialization;

namespace Yomikaze.Application.Data.Models.Common;

public class ComicModel
{
    public long Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? Cover { get; set; }
    public string? Banner { get; set; }
    public DateTimeOffset? Published { get; set; }
    public DateTimeOffset? Ended { get; set; }
    public virtual string? Aliases { get; set; }
    public virtual string? Authors { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual ICollection<GenreModel>? Genres { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual ICollection<ChapterModel>? Chapters { get; set; }
}