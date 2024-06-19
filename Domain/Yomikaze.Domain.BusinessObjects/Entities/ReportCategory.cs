namespace Yomikaze.Domain.Entities;

public class ReportCategory : BaseEntity
{
    [StringLength(128)] public string Name { get; set; } = default!;

    public bool RequiresDescription { get; set; } = false;
}