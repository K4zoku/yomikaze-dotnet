using Yomikaze.Domain.Entities.Weak;

namespace Yomikaze.Domain.Entities;

[Index(nameof(ComicId), nameof(UserId), IsUnique = true)]
public class LibraryEntry : BaseEntity
{
    #region Fields

    private Comic _comic = default!;
    private IList<LibraryCategory> _categories = [];

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
    
    public IList<LibraryEntryCategory> LibraryCategories { get; set; } = new List<LibraryEntryCategory>();

    [DeleteBehavior(DeleteBehavior.Cascade)]
    public IList<LibraryCategory> Categories
    {
        get => LazyLoader.Load(this, ref _categories);
        set => _categories = value;
    }

    #endregion

    #region Constructors

    public LibraryEntry() { }

    public LibraryEntry(Action<object, string>? lazyLoader)
    {
        LazyLoader = lazyLoader;
    }

    #endregion
}