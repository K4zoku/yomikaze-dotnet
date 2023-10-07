using Yomikaze.Domain.Common;

namespace Yomikaze.Domain.Entities;

public class Comic : BaseAuditableEntity<Snowflake>
{
    public string Name { get; set; } = default!;
    public IList<string> Aliases { get; private set; } = new List<string>();
    public IList<string> Authors { get; private set; } = new List<string>();
    public string? Description { get; set; }
    public string? Cover { get; set; }
    public string? Banner { get; set; }
    public DateTimeOffset? Published { get; set; }
    public DateTimeOffset? Ended { get; set; }
    public IList<Genre> Genres { get; private set; } = new List<Genre>();
    public IList<Chapter> Chapters { get; private set; } = new List<Chapter>();
}