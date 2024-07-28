namespace Yomikaze.Domain.Entities;

public abstract class CommentReport : Report
{
    public CommentReportReason Reason { get; set; } = default!;
    public ulong ReasonId { get; set; }
    public ulong CommentId { get; set; }
}