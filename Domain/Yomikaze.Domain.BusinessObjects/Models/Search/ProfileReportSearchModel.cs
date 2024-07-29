using Yomikaze.Domain.Entities;

namespace Yomikaze.Domain.Models.Search;

public class ProfileReportSearchModel
{
    public string? ReasonId { get; set; }
    
    public string? ProfileId { get; set; }
    
    public string? ReporterId { get; set; }
    
    public ReportStatus? Status { get; set; }
    
    public ProfileReportOrderBy[] OrderBy { get; set; } = [];
}

public enum ProfileReportOrderBy
{
    CreationTime,
    CreationTimeDesc,
}