using System.ComponentModel.DataAnnotations;

namespace Abstracts;

public interface IEntity<TId>
{
    [Key] public TId Id { get; set; }
}

public interface IEntity : IEntity<long>
{
}