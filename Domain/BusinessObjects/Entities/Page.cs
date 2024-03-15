using Abstracts;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Yomikaze.Domain.Entities;

public class Page : BaseEntity
{
    public int Index { get; set; }

    public string Image { get; set; } = default!;

    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public virtual Chapter Chapter { get; set; } = default!;
}