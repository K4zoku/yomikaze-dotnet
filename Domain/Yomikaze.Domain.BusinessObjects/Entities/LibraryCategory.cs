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
    
    public new ulong Id { get; }

    #endregion

    #region Constructors

    public LibraryCategory() { }

    public LibraryCategory(ulong id)
    {
        Id = id;
    }

    public LibraryCategory(Action<object, string>? lazyLoader)
    {
        LazyLoader = lazyLoader;
    }

    #endregion
}