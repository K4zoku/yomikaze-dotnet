namespace Yomikaze.Domain.Models;

public class ComicCommentSearchModel
{
    [SwaggerIgnore]
    public ulong? ComicId { get; set; }
    public ComicCommentOrderBy[]? OrderBy { get; set; } 
}

public enum ComicCommentOrderBy
{
    CreationTime,
    CreationTimeDesc,
    LastModified,
    LastModifiedDesc,
}