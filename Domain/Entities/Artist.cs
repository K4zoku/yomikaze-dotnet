using Yomikaze.Domain.Common;

namespace Yomikaze.Domain.Entities;

public class Artist : BaseEntity, IEntity
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? Avatar { get; set; }
    public virtual ICollection<Comic> Comics { get; set; } = new List<Comic>();
}