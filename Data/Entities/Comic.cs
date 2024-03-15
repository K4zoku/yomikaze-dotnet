using Microsoft.EntityFrameworkCore;
using Yomikaze.Domain.Common;

namespace Yomikaze.Domain.Entities;

public class Comic : BaseEntity
{
    public required string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? Cover { get; set; }
    public string? Banner { get; set; }
    public DateTimeOffset? Published { get; set; }
    public DateTimeOffset? Ended { get; set; }

    public virtual string? Aliases { get; set; } = default!;
    public virtual string? Authors { get; set; } = default!;

    [DeleteBehavior(DeleteBehavior.Cascade)]
    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();

    [DeleteBehavior(DeleteBehavior.Cascade)]
    public virtual ICollection<Chapter> Chapters { get; set; } = new List<Chapter>();
}