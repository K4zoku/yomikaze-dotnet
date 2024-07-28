namespace Yomikaze.Domain.Models;

public class ComicCommentSearchModel
{
    [SwaggerIgnore]
    public ulong? ComicId { get; set; }
    
    [SwaggerIgnore]
    public ulong? ReplyToId { get; set; }

    public ComicCommentOrderBy[] OrderBy { get; set; } = [];
}

public enum ComicCommentOrderBy
{
    CreationTime,
    CreationTimeDesc,
    LastModified,
    LastModifiedDesc,
}