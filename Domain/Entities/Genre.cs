using Yomikaze.Domain.Common;

namespace Yomikaze.Domain.Entities;

public class Genre : BaseEntity<long>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public ICollection<Comic> Comics { get; private set; } = new List<Comic>();
}