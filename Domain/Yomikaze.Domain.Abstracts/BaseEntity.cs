using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Yomikaze.Domain.Abstracts;

[PrimaryKey(nameof(Id))]
public abstract class BaseEntity<TId> : IEntity<TId>
{
    [Column( Order = 98)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTimeOffset CreationTime { get; set; } = DateTimeOffset.UtcNow;
    
    [Column(Order = 99)]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [Timestamp]
    public DateTimeOffset? LastModified { get; set; }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual TId Id { get; init; } = default!;

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
    [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
    protected BaseEntity()
    {
        Id = SnowflakeGenerator.Generate(WorkerId);
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public new ulong Id { get; }

    [NotMapped] public virtual int WorkerId => 0;
}