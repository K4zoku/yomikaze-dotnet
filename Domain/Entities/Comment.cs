using System.ComponentModel.DataAnnotations.Schema;
using Yomikaze.Domain.Common;

namespace Yomikaze.Domain.Entities;

public class Comment : BaseEntity
{
    public string Content { get; set; } = default!;

    [ForeignKey(nameof(User))] public virtual long UserId { get; set; }

    public virtual User User { get; set; } = default!;

    [ForeignKey(nameof(Comic))] public long ComicId { get; set; }

    public virtual Comic Comic { get; set; } = default!;

    public virtual Comment? ReplyTo { get; set; }

    [InverseProperty(nameof(ReplyTo))] public virtual ICollection<Comment> Replies { get; set; } = new List<Comment>();

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}