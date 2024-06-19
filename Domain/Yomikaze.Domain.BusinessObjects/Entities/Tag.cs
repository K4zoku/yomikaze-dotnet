namespace Yomikaze.Domain.Entities;

[Index(nameof(Name), nameof(CategoryId), IsUnique = true)]
public class Tag : BaseEntity
{
    #region Fields

    private ICollection<Comic> _comics = [];
    private TagCategory _category = default!;

    #endregion

    #region Properties

    private Action<object, string>? LazyLoader { get; }

    [StringLength(64)] public string Name { get; set; } = default!;

    [StringLength(512)] public string? Description { get; set; }

    public ICollection<Comic> Comics
    {
        get => LazyLoader.Load(this, ref _comics);
        set => _comics = value;
    }

    [ForeignKey(nameof(Category))] public ulong CategoryId { get; set; }

    [DeleteBehavior(DeleteBehavior.Cascade)]
    public TagCategory Category
    {
        get => LazyLoader.Load(this, ref _category);
        init => _category = value;
    }

    #endregion

    #region Constructors

    public Tag() { }

    public Tag(Action<object, string>? lazyLoader)
    {
        LazyLoader = lazyLoader;
    }

    #endregion
}