using Microsoft.EntityFrameworkCore;
using Yomikaze.Domain.Common;
using Yomikaze.Domain.Database.Entities.Identity;

namespace Yomikaze.Domain.Database.Entities;

public class HistoryRecord : BaseEntity, IEntity
{
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public virtual Chapter Chapter { get; set; } = default!;

    [DeleteBehavior(DeleteBehavior.Cascade)]
    public virtual User User { get; set; } = default!;

    public DateTimeOffset LastRead { get; set; }
}