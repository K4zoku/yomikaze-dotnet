using Abstracts;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yomikaze.Domain.Entities;

public class HistoryRecord : BaseEntity
{
    [ForeignKey(nameof(Chapter))]
    public long ChapterId { get; set; }

    [DeleteBehavior(DeleteBehavior.Cascade)]
    public virtual Chapter Chapter { get; set; } = default!;

    [ForeignKey(nameof(User))] public long UserId { get; set; }

    [DeleteBehavior(DeleteBehavior.Cascade)]
    public virtual User User { get; set; } = default!;

    public DateTimeOffset LastRead { get; set; } = DateTimeOffset.Now;
}