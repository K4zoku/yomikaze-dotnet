using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Yomikaze.Domain.Abstracts;

public interface IEntity<TId>
{
    [Key] public TId Id { get; set; }
}

public interface IEntity : IEntity<ulong>
{
    [NotMapped]
    [DataMember(Name = "idStr")]
    public string IdString => Id.ToString();

    [NotMapped] public int WorkerId => 0;
}