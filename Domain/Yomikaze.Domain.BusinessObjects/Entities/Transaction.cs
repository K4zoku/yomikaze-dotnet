namespace Yomikaze.Domain.Entities;

public class Transaction : BaseEntity
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

    public long Amount { get; set; } = default!;

    public string Description { get; set; } = default!;

    #endregion

    #region Constructors

    public Transaction() { }

    public Transaction(Action<object, string>? lazyLoader)
    {
        LazyLoader = lazyLoader;
    }

    #endregion
}