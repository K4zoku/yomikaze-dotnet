using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Yomikaze.Domain.Common;

[PrimaryKey(nameof(Id))]
public abstract class BaseEntity<TId> : IEntity<TId>
{
    [Key]
    public TId Id { get; } = default!;
}
public abstract class BaseEntity : BaseEntity<long>
{
}