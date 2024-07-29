using Yomikaze.Domain.Entities;

namespace Yomikaze.Domain.Models.Search;

public class ChapterReportSearchModel
{
    public string? ReasonId { get; set; }
    
    public string? ComicId { get; set; }
    
    public int? ChapterNumber { get; set; }
    
    public string? ReporterId { get; set; }
    
    public ReportStatus? Status { get; set; }
    
    public ChapterReportOrderBy[] OrderBy { get; set; } = [];
}

public enum ChapterReportOrderBy
{
    CreationTime,
    CreationTimeDesc,
}