using Yomikaze.Domain.Common;
using Yomikaze.Domain.Database.Entities.Identity;

namespace Yomikaze.Domain.Database.Entities;

public class Comment : BaseEntity, IEntity 
{
    public string Content { get; set; } = default!;
    
    public virtual User User { get; set; } = default!;
    
    public virtual Comic Comic { get; set; } = default!;
    
    public virtual Comment? ReplyTo { get; set; }
    
    public virtual ICollection<Comment> Replies { get; set; } = new List<Comment>();
    
    public DateTimeOffset CreatedAt { get; set; }
    
    public DateTimeOffset? UpdatedAt { get; set; }
}