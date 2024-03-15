using Yomikaze.Domain.Common;

namespace Yomikaze.Domain.Entities;

public class Notification : BaseEntity
{
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public bool IsRead { get; set; }

    public virtual User User { get; set; } = default!;
}