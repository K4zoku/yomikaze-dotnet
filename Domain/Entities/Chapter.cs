using Yomikaze.Domain.Common;

namespace Yomikaze.Domain.Entities;

public class Chapter : BaseAuditableEntity<Snowflake>
{
    public int Index { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public IList<string> Pages { get; private set; } = new List<string>();
    public decimal? Price { get; set; }
    public DateTimeOffset? Available { get; set; }
    public IList<User> PurchasedAccounts { get; private set; } = new List<User>();
    public Comic Comic { get; set; } = default!;
}