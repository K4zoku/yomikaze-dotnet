using System.ComponentModel.DataAnnotations;

namespace Yomikaze.Domain.Common;

public interface  IEntity<out TId>
{
    [Key]
    public TId Id { get; }
}

public interface IEntity : IEntity<long>
{
}