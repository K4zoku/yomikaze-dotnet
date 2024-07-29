namespace Yomikaze.Domain.Models.Search;

public class ChapterSearchModel
{
    
    public ChapterOrderBy[] OrderBy { get; set; } = [];
    
    public bool? IsUnlocked { get; set; }
    
    public bool? HasLock { get; set; }
    
    public bool Pagination { get; set; } = false; // Default to false to prevent unnecessary pagination
}

public enum ChapterOrderBy
{
    Number,
    NumberDesc,
    Name,
    NameDesc,
    CreationTime,
    CreationTimeDesc,
    LastModified,
    LastModifiedDesc,
}