using Yomikaze.Domain.Common;
using Yomikaze.Domain.Database.Entities.Identity;

namespace Yomikaze.Domain.Database.Entities;

public class Notification : BaseEntity, IEntity
{
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public DateTimeOffset CreatedAt { get; set; }
    public bool IsRead { get; set; }
    
    public virtual YomikazeUser User { get; set; } = default!;
}