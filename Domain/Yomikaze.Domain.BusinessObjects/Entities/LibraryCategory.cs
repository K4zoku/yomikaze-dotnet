using Yomikaze.Domain.Entities.Weak;

namespace Yomikaze.Domain.Entities;

[Index(nameof(UserId), nameof(Name), IsUnique = true)]
public class LibraryCategory : BaseEntity
{
    #region Fields

    private User _user = default!;

    #endregion

    #region Properties

    private Action<object, string>? LazyLoader { get; }

    [ForeignKey(nameof(User))] public ulong UserId { get; set; }

    [DeleteBehavior(DeleteBehavior.Cascade)]
    public User User
    {
        get => LazyLoader.Load(this, ref _user);
        set => _user = value;
    }

    [StringLength(32)] public string Name { get; set; } = default!;
    
    public ICollection<LibraryEntry> Entries { get; set; } = new List<LibraryEntry>();

    #endregion

    #region Constructors

    public LibraryCategory() { }

    public LibraryCategory(Action<object, string>? lazyLoader)
    {
        LazyLoader = lazyLoader;
    }

    #endregion
}