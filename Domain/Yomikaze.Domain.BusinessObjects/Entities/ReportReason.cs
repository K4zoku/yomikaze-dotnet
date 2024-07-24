namespace Yomikaze.Domain.Entities;

public class ReportReason : BaseEntity
{
    [StringLength(128)] public string Content { get; set; } = default!;

    public override ulong Id { get; }
    
    public ReportReason(ulong id, string content)
    {
        Id = id;
        Content = content;
    }
}