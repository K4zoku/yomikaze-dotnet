using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yomikaze.Domain.Abstracts;

[PrimaryKey(nameof(Id))]
public abstract class BaseEntity<TId> : IEntity<TId>
{
    public DateTimeOffset? CreationTime { get; set; }

    public DateTimeOffset? LastModified { get; set; }

    [Key] public virtual TId Id { get; } = default!;

    public override bool Equals(object? obj)
    {
        return obj is BaseEntity<TId> other && Equals(Id, other.Id);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }
}

[PrimaryKey(nameof(Id))]
public abstract class BaseEntity : BaseEntity<ulong>, IEntity
{
    [NotMapped] public virtual int WorkerId => 0;
}