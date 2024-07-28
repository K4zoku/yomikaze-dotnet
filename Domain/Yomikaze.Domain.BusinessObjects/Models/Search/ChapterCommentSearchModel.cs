namespace Yomikaze.Domain.Models;

public class ChapterCommentSearchModel : ComicCommentSearchModel
{
    [SwaggerIgnore]
    public ulong? ChapterId { get; set; }
    
    [SwaggerIgnore]
    public int? ChapterNumber { get; set; }
    
}