namespace Yomikaze.Domain.Entities;

public class ProfileComment : Comment
{
    #region Fields

    private User _profile = default!;

    #endregion

    #region Properties

    [ForeignKey(nameof(Profile))] public ulong ProfileId { get; set; }

    public User Profile
    {
        get => LazyLoader.Load(this, ref _profile);
        set => _profile = value;
    }

    #endregion

    #region Constructors

    public ProfileComment()
    {
    }

    public ProfileComment(Action<object, string> lazyLoader) : base(lazyLoader)
    {
    }

    #endregion
}