namespace Yomikaze.Domain.Entities;

public class WithdrawalRequest : BaseEntity
{
    #region Fields

    private User _profile = default!;

    #endregion

    #region MiscProperties

    private Action<object, string>? LazyLoader { get; }

    #endregion

    #region Properties

    public string UserId { get; set; } = default!;

    #endregion

    #region Navigation Properties

    public User Profile
    {
        get => LazyLoader.Load(this, ref _profile);
        set => _profile = value;
    }

    #endregion

    #region Constructors

    public WithdrawalRequest()
    {
    }

    public WithdrawalRequest(Action<object, string>? lazyLoader)
    {
        LazyLoader = lazyLoader;
    }

    #endregion
}

public enum WithdrawalRequestStatus
{
    Pending = 0,
    Approved = 1,
    Rejected = 2
}