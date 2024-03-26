using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Yomikaze.Domain.Abstracts;

namespace Yomikaze.Domain.Entities;

[Table("notifications")]
[DataContract(Name = "notification")]
public class Notification : BaseEntity
{
    #region Properties

    [StringLength(256)]
    [DataMember(Name = "title")]
    [Column("title", Order = 1)]    
    public string Title { get; set; } = default!;

    [StringLength(512)]
    [DataMember(Name = "content")]
    [Column("content", Order = 2)]
    public string Content { get; set; } = default!;

    [DataMember(Name = "read")]
    [Column("read", Order = 3)]
    public bool Read { get; set; }

    [StringLength(20)]
    [DataMember(Name = "userId")]
    [Column("user_id", Order = 4)]
    public string UserId { get; set; } = default!;

    #endregion
}