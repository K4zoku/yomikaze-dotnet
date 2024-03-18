using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yomikaze.Domain.Abstracts;

[PrimaryKey(nameof(Id))]
public abstract class BaseEntity<TId> : IEntity<TId>
{
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTimeOffset? LastUpdated { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTimeOffset CreatedAt { get; set; } = DateTime.UtcNow;

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public virtual TId Id { get; set; } = default!;
}

public abstract class BaseEntity : BaseEntity<ulong>, IEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override ulong Id { get; set; } = SnowflakeGenerator.Generate();
}