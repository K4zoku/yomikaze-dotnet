using Microsoft.EntityFrameworkCore;
using Yomikaze.Domain.Common;

namespace Yomikaze.Domain.Entities;

public class HistoryRecord : BaseEntity
{
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public virtual Chapter Chapter { get; set; } = default!;

    [DeleteBehavior(DeleteBehavior.Cascade)]
    public virtual User User { get; set; } = default!;

    public DateTimeOffset LastRead { get; set; } = DateTimeOffset.Now;
}