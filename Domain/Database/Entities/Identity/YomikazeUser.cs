using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Yomikaze.Domain.Common;

namespace Yomikaze.Domain.Database.Entities.Identity;

[PrimaryKey(nameof(Id))]
public class YomikazeUser : IdentityUser<long>, IEntity
{
    public new long Id { get; set; }
    
    public string? Avatar { get; set; }
    
    public string? Banner { get; set; }
    
    public string? Bio { get; set; }
    
    public string? Fullname { get; set; }
    
    public DateTimeOffset Birthday { get; set; }
    
    public virtual ICollection<Comic> Library { get; set; } = new List<Comic>();
    
    public virtual ICollection<HistoryRecord> History { get; set; } = new List<HistoryRecord>();
    
}