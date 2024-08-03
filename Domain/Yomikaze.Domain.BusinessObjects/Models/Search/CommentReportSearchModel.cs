using Yomikaze.Domain.Entities;

namespace Yomikaze.Domain.Models.Search;

public class CommentReportSearchModel
{
    public string? ReasonId { get; set; }

    public string? CommentId { get; set; }

    public string? ReporterId { get; set; }

    public ReportStatus? Status { get; set; }

    public CommentReportOrderBy[] OrderBy { get; set; } = [];
}

public enum CommentReportOrderBy
{
    CreationTime,
    CreationTimeDesc
}