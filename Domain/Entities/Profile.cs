using Yomikaze.Domain.Common;

namespace Yomikaze.Domain.Entities;

public class Profile : BaseEntity, IEntity
{
    public string AuthId { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Bio { get; set; }
    public string? Avatar { get; set; }
    public string? Banner { get; set; }
    public decimal Balance { get; set; }
    
    public virtual ICollection<Comic> Library { get; set; } = new List<Comic>();
}