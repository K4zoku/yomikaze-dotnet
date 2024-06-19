namespace Yomikaze.Domain.Models;

public class CommentModel : BaseModel
{
    #region ReadWriteProperties

    public string? Content { get; set; } = default!;

    #endregion

    #region ReadOnlyProperties
    
    public ProfileModel Author { get; set; } = default!;
    
    public ICollection<CommentModel> Replies { get; set; } = new List<CommentModel>();
    
    #endregion

    #region WriteOnlyProperties

    public string? AuthorId { get; set; } = default!;

    public string? ReplyToId { get; set; }

    #endregion
}
