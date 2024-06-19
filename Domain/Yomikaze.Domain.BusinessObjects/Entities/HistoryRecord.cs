namespace Yomikaze.Domain.Entities;

public class HistoryRecord : BaseEntity
{
    #region Fields

    private Chapter _chapter = default!;

    #endregion

    #region Properties

    private Action<object, string>? LazyLoader { get; }

    [ForeignKey(nameof(Chapter))] public ulong ChapterId { get; set; }

    [DeleteBehavior(DeleteBehavior.Cascade)]
    public Chapter Chapter
    {
        get => LazyLoader.Load(this, ref _chapter);
        set => _chapter = value;
    }

    public int PageNumber { get; set; } = 1;

    [ForeignKey(nameof(User))] public ulong UserId { get; set; }


    [DeleteBehavior(DeleteBehavior.Cascade)]
    public User User { get; set; } = default!;

    #endregion

    #region Constructors

    public HistoryRecord()
    {
        LastModified = DateTime.UtcNow;
    }

    public HistoryRecord(Action<object, string>? lazyLoader)
    {
        LazyLoader = lazyLoader;
        LastModified = DateTime.UtcNow;
    }

    #endregion
}