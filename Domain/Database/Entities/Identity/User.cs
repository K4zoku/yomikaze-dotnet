using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Yomikaze.Domain.Common;

namespace Yomikaze.Domain.Database.Entities.Identity;

public class User : IdentityUser<long>, IEntity
{
    public string? Avatar { get; set; }

    public string? Banner { get; set; }

    public string? Bio { get; set; }

    public string? Fullname { get; set; }

    public DateTimeOffset Birthday { get; set; }

    [DeleteBehavior(DeleteBehavior.Cascade)]
    public virtual ICollection<Comic> Library { get; set; } = new List<Comic>();

    [DeleteBehavior(DeleteBehavior.Cascade)]
    public virtual ICollection<HistoryRecord> History { get; set; } = new List<HistoryRecord>();

}