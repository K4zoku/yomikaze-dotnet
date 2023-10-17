using Yomikaze.Domain.Common;

namespace Yomikaze.Domain.Database.Entities;

public class Genre : BaseEntity, IEntity
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public virtual ICollection<Comic> Comics { get; private set; } = new List<Comic>();
}