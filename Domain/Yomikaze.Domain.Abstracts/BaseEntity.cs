using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Yomikaze.Domain.Abstracts;

[PrimaryKey(nameof(Id))]
public abstract class BaseEntity<TId> : IEntity<TId>
{
    [DataMember(Name = "creationTime")]
    [Column("creation_time", Order = 98)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTimeOffset CreationTime { get; set; } = DateTimeOffset.UtcNow;

    [DataMember(Name = "lastModified")]
    [Column("last_modified", Order = 99)]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [Timestamp]
    public DateTimeOffset? LastModified { get; set; }

    [Key]
    [DataMember(Name = "id")]
    [Column("id", Order = 0)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual TId Id { get; set; } = default!;
}

[PrimaryKey(nameof(Id))]
public abstract class BaseEntity : BaseEntity<ulong>, IEntity
{
    protected BaseEntity(int workerId = 0)
    {
        Id = SnowflakeGenerator.Generate(workerId);
    }

    [NotMapped]
    [DataMember(Name = "idStr")]
    public string IdStr => Id.ToString();

    [Key]
    [DataMember(Name = "id")]
    [Column("id", Order = 0)]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public new ulong Id { get; set; }

    [NotMapped] public int WorkerId => 0;
}