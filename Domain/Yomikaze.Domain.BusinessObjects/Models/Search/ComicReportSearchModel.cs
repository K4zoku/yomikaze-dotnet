using Yomikaze.Domain.Entities;

namespace Yomikaze.Domain.Models.Search;

public class ComicReportSearchModel
{
    public string? ReasonId { get; set; }

    public string? ComicId { get; set; }

    public string? ReporterId { get; set; }

    public ReportStatus? Status { get; set; }

    public ComicReportOrderBy[] OrderBy { get; set; } = [];
}

public enum ComicReportOrderBy
{
    CreationTime,
    CreationTimeDesc
}