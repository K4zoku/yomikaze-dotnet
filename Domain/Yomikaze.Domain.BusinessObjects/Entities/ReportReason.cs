namespace Yomikaze.Domain.Entities;

public class ReportReason : BaseEntity
{
    public ReportReason(ulong id, string content)
    {
        Id = id;
        Content = content;
    }

    [StringLength(128)] public string Content { get; set; }

    public override ulong Id { get; }
}