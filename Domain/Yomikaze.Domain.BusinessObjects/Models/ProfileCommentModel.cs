namespace Yomikaze.Domain.Models;

public class ProfileCommentModel : CommentModel
{
    #region WriteOnlyProperties
    
    public string? ProfileId { get; set; } = default!;
    
    #endregion
}