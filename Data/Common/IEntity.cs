using System.ComponentModel.DataAnnotations;

namespace Yomikaze.Domain.Common;

public interface IEntity<TId>
{
    [Key] public TId Id { get; set; }
}

public interface IEntity : IEntity<long>
{
}