namespace Yomikaze.Domain.Entities;

public class TagCategory : BaseEntity
{
    public TagCategory()
    {
    }

    public TagCategory(ulong id, string name)
    {
        Id = id;
        Name = name;
    }

    public override ulong Id { get; }

    [StringLength(64)] public string Name { get; set; } = default!;
}