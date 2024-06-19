namespace Yomikaze.Domain.Entities;

public class TranslationReport : Report
{
    #region Fields

    private Translation _translation = default!;

    #endregion

    #region Properties

    [ForeignKey(nameof(Translation))] public ulong TranslationId { get; set; }

    public Translation Translation
    {
        get => LazyLoader.Load(this, ref _translation);
        set => _translation = value;
    }

    #endregion

    #region Constructors

    public TranslationReport()
    {
    }

    public TranslationReport(Action<object, string> lazyLoader) : base(lazyLoader)
    {
    }

    #endregion
}