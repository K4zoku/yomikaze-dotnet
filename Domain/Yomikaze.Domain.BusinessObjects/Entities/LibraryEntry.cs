namespace Yomikaze.Domain.Entities;

[Index(nameof(ComicId), nameof(UserId), IsUnique = true)]
public class LibraryEntry : BaseEntity
{
    #region Fields

    private Comic _comic = default!;
    private LibraryCategory? _category;

    #endregion

    #region Properties

    private Action<object, string>? LazyLoader { get; }

    [ForeignKey(nameof(Comic))]
    [DataMember(Name = "comicId", Order = 1)]
    [Column("comic_id", Order = 1)]
    public ulong ComicId { get; set; }

    [DataMember(Name = "comic")]
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public Comic Comic
    {
        get => LazyLoader.Load(this, ref _comic);
        set => _comic = value;
    }

    [ForeignKey(nameof(User))]
    [DataMember(Name = "userId", Order = 2)]
    [Column("user_id", Order = 2)]
    public ulong UserId { get; set; }

    [DeleteBehavior(DeleteBehavior.Cascade)]
    public User User { get; set; } = default!;

    [ForeignKey(nameof(Category))]
    [DataMember(Name = "categoryId", Order = 3)]
    [Column("category_id", Order = 3)]
    public ulong? CategoryId { get; set; }

    [DeleteBehavior(DeleteBehavior.Cascade)]
    public LibraryCategory? Category
    {
        get => LazyLoader.LoadNullable(this, ref _category);
        set => _category = value;
    }

    [NotMapped] public string CategoryName => Category?.Name ?? "Uncategorized";

    #endregion

    #region Constructors

    public LibraryEntry() { }

    public LibraryEntry(Action<object, string>? lazyLoader)
    {
        LazyLoader = lazyLoader;
    }

    #endregion
}