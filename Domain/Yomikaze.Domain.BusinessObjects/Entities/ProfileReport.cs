namespace Yomikaze.Domain.Entities;

public class ProfileReport : Report
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

    public override ProfileReportReason Reason { get; } = default!;

    #endregion

    #region Constructors

    public ProfileReport()
    {
    }

    public ProfileReport(Action<object, string> lazyLoader) : base(lazyLoader)
    {
    }

    #endregion
}