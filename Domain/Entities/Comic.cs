using Yomikaze.Domain.Common;

namespace Yomikaze.Domain.Entities;

public class Comic : BaseEntity, IEntity
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? Cover { get; set; }
    public string? Banner { get; set; }
    public DateTimeOffset? Published { get; set; }
    public DateTimeOffset? Ended { get; set; }
    
    public virtual ICollection<Alias> Aliases { get; set; } = new List<Alias>();
    public virtual ICollection<Artist> Authors { get; set; } = new List<Artist>();
    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
    public virtual ICollection<Chapter> Chapters { get; set; } = new List<Chapter>();
}