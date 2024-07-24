namespace Yomikaze.Domain.Entities;

public class ComicReport : Report
{
    #region Fields

    private Comic _comic = default!;

    #endregion

    #region Properties

    [ForeignKey(nameof(Comic))] public ulong ComicId { get; set; }

    public Comic Comic
    {
        get => LazyLoader.Load(this, ref _comic);
        set => _comic = value;
    }
    
    public override ComicReportReason Reason { get; } = default!;

    #endregion

    #region Constructors

    public ComicReport()
    {
    }

    public ComicReport(Action<object, string> lazyLoader) : base(lazyLoader)
    {
    }

    #endregion
}