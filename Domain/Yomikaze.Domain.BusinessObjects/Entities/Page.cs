namespace Yomikaze.Domain.Entities;

[Table("pages")]
[DataContract(Name = "page")]
public class Page : BaseEntity
{
    #region Fields

    private Chapter _chapter = default!;

    #endregion

    #region Properties

    private Action<object, string>? LazyLoader { get; }
    public int Number { get; set; }

    [StringLength(512)] public string Image { get; set; } = default!;

    [ForeignKey(nameof(Chapter))] public ulong ChapterId { get; set; }

    [DeleteBehavior(DeleteBehavior.Cascade)]
    public Chapter Chapter
    {
        get => LazyLoader.Load(this, ref _chapter);
        set => _chapter = value;
    }

    #endregion

    #region Constructors

    public Page()
    {
    }

    public Page(Action<object, string>? lazyLoader)
    {
        LazyLoader = lazyLoader;
    }

    #endregion
}