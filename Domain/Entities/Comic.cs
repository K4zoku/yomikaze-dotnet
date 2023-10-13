using Yomikaze.Domain.Common;

namespace Yomikaze.Domain.Entities;

public class Comic : BaseAuditableEntity<long>
{
    public string Name { get; set; } = default!;
    public ICollection<string> Aliases { get; private set; } = new List<string>();
    public ICollection<string> Authors { get; private set; } = new List<string>();
    public string? Description { get; set; }
    public string? Cover { get; set; }
    public string? Banner { get; set; }
    public DateTimeOffset? Published { get; set; }
    public DateTimeOffset? Ended { get; set; }
    public ICollection<Genre> Genres { get; private set; } = new List<Genre>();
    public ICollection<Chapter> Chapters { get; private set; } = new List<Chapter>();
}