using Yomikaze.Domain.Entities;

namespace Yomikaze.Domain.Models;

public class ComicReportSearchModel
{
    public string? ReportCategoryId { get; set; }
    
    public string? ComicId { get; set; }
    
    public string? ReporterId { get; set; }
    
    public ReportStatus? Status { get; set; }
    
    public ComicReportOrderBy[] OrderBy { get; set; } = [];
}

public enum ComicReportOrderBy
{
    CreationTime,
    CreationTimeDesc,
}