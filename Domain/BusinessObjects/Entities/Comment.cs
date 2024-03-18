using System.ComponentModel.DataAnnotations.Schema;
using Yomikaze.Domain.Abstracts;

namespace Yomikaze.Domain.Entities;

public class Comment : BaseEntity
{
    public string Content { get; set; } = default!;

    [ForeignKey(nameof(User))] public virtual string UserId { get; set; }

    public virtual User User { get; set; } = default!;

    [ForeignKey(nameof(Comic))] public string ComicId { get; set; }

    public virtual Comic Comic { get; set; } = default!;

    public virtual Comment? ReplyTo { get; set; }

    [InverseProperty(nameof(ReplyTo))] public virtual ICollection<Comment> Replies { get; set; } = new List<Comment>();
}