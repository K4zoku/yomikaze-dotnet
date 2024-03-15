using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Abstracts;

[PrimaryKey(nameof(Id))]
public abstract class BaseEntity<TId> : IEntity<TId>
{
    [Key] public TId Id { get; set; } = default!;
}

public abstract class BaseEntity : BaseEntity<long>, IEntity
{
}