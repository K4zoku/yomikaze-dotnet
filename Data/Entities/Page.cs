using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Yomikaze.Domain.Common;

namespace Yomikaze.Domain.Entities;

[Table("Page")]
public class Page : BaseEntity
{
    public int Index { get; set; }

    public string Image { get; set; } = default!;
    
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public virtual Chapter Chapter { get; set; } = default!;
}