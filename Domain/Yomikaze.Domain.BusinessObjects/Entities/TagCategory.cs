namespace Yomikaze.Domain.Entities;

public class TagCategory : BaseEntity
{
    [StringLength(64)]
    public string Name { get; set; } = default!;
}