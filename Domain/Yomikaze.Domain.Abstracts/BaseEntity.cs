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
    public DateTimeOffset CreationTime { get; set; } = DateTime.UtcNow;

    [DataMember(Name = "lastModified")]
    [Column("last_modified", Order = 99)]
    public DateTimeOffset? LastModified { get; set; }

    [Key]
    [DataMember(Name = "id")]
    [Column("id", Order = 0)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual TId Id { get; set; } = default!;
}

[PrimaryKey(nameof(Id))]
public abstract class BaseEntity : BaseEntity<string>, IEntity
{
    [Key]
    [StringLength(20)]
    [DataMember(Name = "id")]
    [Column("id", Order = 0)]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public override string Id { get; set; } = SnowflakeGenerator.Generate();
}