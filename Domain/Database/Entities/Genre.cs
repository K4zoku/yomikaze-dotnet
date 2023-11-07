using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Yomikaze.Domain.Common;

namespace Yomikaze.Domain.Database.Entities;

[Index(nameof(Name), IsUnique = true)]
public class Genre : BaseEntity, IEntity
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public virtual ICollection<Comic> Comics { get; private set; } = new List<Comic>();
}