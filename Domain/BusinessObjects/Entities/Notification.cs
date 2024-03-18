using Yomikaze.Domain.Abstracts;

namespace Yomikaze.Domain.Entities;

public class Notification : BaseEntity
{
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public bool Read { get; set; } = false;
    public virtual User User { get; set; } = default!;
}