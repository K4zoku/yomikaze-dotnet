using Yomikaze.Domain.Common;

namespace Yomikaze.Domain.Entities;

public class Transaction : BaseAuditableEntity<Snowflake>
{
    public User User { get; set; } = default!;
    public decimal Amount { get; set; }
    public string? Description { get; set; }
}