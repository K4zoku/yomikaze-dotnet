namespace Yomikaze.Domain.Entities;

public class CommentReport : Report
{
    public override CommentReportReason Reason { get; } = default!;
    public ulong CommentId { get; set; } = default!;
}