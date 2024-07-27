using Yomikaze.Domain.Entities.Weak;

namespace Yomikaze.Domain.Models;

public class CommentModel : BaseModel
{
    #region CommonProperties

    [StringLength(1024)]
    public string? Content { get; set; } = default!;

    #endregion

    #region ReadOnlyProperties

    [SwaggerSchema(ReadOnly = true)]
    public ProfileModel? Author { get; set; }
    
    [SwaggerSchema(ReadOnly = true)]
    public int TotalLikes { get; set; }
    
    [SwaggerSchema(ReadOnly = true)]
    public int TotalDislikes { get; set; }
    
    [SwaggerSchema(ReadOnly = true)]
    public bool IsReacted { get; set; }
    
    [SwaggerSchema(ReadOnly = true)]
    public ReactionType? MyReaction { get; set; } 
    
    [SwaggerSchema(ReadOnly = true)]
    public int TotalReplies { get; set; }
    
    #endregion

    #region WriteOnlyProperties

    [SwaggerIgnore]
    [SwaggerSchema(ReadOnly = true)]
    [WriteOnly]
    public string? AuthorId { get; set; } = default!;

    [SwaggerIgnore]
    [SwaggerSchema(ReadOnly = true)]
    [WriteOnly]
    public string? ReplyToId { get; set; }

    #endregion
}