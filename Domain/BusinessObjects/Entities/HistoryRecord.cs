using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Yomikaze.Domain.Abstracts;

namespace Yomikaze.Domain.Entities;

public class HistoryRecord : BaseEntity
{
    [ForeignKey(nameof(Chapter))] public string ChapterId { get; set; }

    [DeleteBehavior(DeleteBehavior.Cascade)]
    public virtual Chapter Chapter { get; set; } = default!;

    [ForeignKey(nameof(User))] public string UserId { get; set; }

    [DeleteBehavior(DeleteBehavior.Cascade)]
    public virtual User User { get; set; } = default!;
}