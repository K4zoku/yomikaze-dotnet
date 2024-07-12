namespace Yomikaze.Domain.Models;

public class HistorySearchModel
{
    public HistoryOrderBy[]? OrderBy { get; set; }
    
    public DateTime? FromCreationTime { get; set; }
    
    public DateTime? ToCreationTime { get; set; }
}

public enum HistoryOrderBy
{
    PageNumber,
    PageNumberDesc,
    CreationTime,
    CreationTimeDesc,
    LastModified,
    LastModifiedDesc
}