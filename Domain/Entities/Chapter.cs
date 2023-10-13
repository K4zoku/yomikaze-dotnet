using System.ComponentModel.DataAnnotations.Schema;
using Yomikaze.Domain.Common;

namespace Yomikaze.Domain.Entities;

public class Chapter : BaseAuditableEntity<long>
{
    public int Index { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public ICollection<Page> Pages { get; private set; } = new List<Page>();
    public decimal? Price { get; set; }
    public DateTimeOffset? Available { get; set; }
    public ICollection<User> PurchasedAccounts { get; private set; } = new List<User>();
    
    
    [ForeignKey(nameof(Comic))]
    public long ComicId { get; set; }
    
    public Comic Comic { get; set; } = default!;
}