using Yomikaze.Domain.Common;
using Yomikaze.Domain.Database.Entities.Identity;

namespace Yomikaze.Domain.Database.Entities;

public class HistoryRecord : BaseEntity, IEntity
{
    public virtual Chapter Chapter { get; set; } = default!;
    
    public virtual YomikazeUser User { get; set; } = default!;
    
    public DateTimeOffset LastRead { get; set; }
    
    public int PageIndex { get; set; }
}