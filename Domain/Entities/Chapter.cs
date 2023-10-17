using System.ComponentModel.DataAnnotations.Schema;
using Yomikaze.Domain.Common;

namespace Yomikaze.Domain.Entities;

public class Chapter : BaseEntity, IEntity
{
    public int Index { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public DateTimeOffset? Available { get; set; }
    
    public virtual ICollection<Page> Pages { get; set; } = new List<Page>();
    public virtual ICollection<Profile> PurchasedAccounts { get; set; } = new List<Profile>();
    public virtual Comic Comic { get; set; } = default!;
}