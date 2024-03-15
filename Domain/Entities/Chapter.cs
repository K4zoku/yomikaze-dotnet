using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Yomikaze.Domain.Common;

namespace Yomikaze.Domain.Entities;

public class Chapter : BaseEntity
{
    public int Index { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public DateTimeOffset? Available { get; set; }

    public virtual ICollection<Page> Pages { get; set; } = new List<Page>();

    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public virtual ICollection<HistoryRecord> Trackers { get; set; } = new List<HistoryRecord>();

    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public virtual Comic Comic { get; set; } = default!;
}