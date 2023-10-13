using System.ComponentModel.DataAnnotations;

namespace Yomikaze.Domain.Common;

public abstract class BaseEntity<TId>
{
    [Key]
    public TId Id { get; set; } = default!;
    public bool IsDeleted { get; set; }
}