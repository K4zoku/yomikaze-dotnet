using Yomikaze.Domain.Common;

namespace Yomikaze.Domain.Entities;

public class Transaction : BaseEntity, IEntity
{
    public virtual Profile Profile { get; set; } = default!;
    public decimal Amount { get; set; }
    public string? Description { get; set; }
}