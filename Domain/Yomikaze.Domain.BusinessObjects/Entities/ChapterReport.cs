namespace Yomikaze.Domain.Entities;

public class ChapterReport : Report
{
    #region Fields

    private Chapter _chapter = default!;

    #endregion

    #region Properties

    [ForeignKey(nameof(Chapter))] public ulong ChapterId { get; set; }

    public Chapter Chapter
    {
        get => LazyLoader.Load(this, ref _chapter);
        set => _chapter = value;
    }

    #endregion

    #region Constructors

    public ChapterReport()
    {
    }

    public ChapterReport(Action<object, string> lazyLoader) : base(lazyLoader)
    {
    }

    #endregion
}