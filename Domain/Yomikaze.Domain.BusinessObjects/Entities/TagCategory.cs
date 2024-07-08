namespace Yomikaze.Domain.Entities;

public class TagCategory : BaseEntity
{
    
    public override ulong Id { get; }
    public TagCategory()
    {
    }

    public TagCategory(ulong id, string name)
    {
        Id = id;
        Name = name;
    }

    [StringLength(64)] public string Name { get; set; } = default!;
}