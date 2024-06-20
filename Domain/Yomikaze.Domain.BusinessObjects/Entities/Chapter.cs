namespace Yomikaze.Domain.Entities;

public class Chapter : BaseEntity
{
    #region Fields

    private Comic _comic = default!;
    private IList<Page> _pages = [];

    #endregion

    #region Properties

    private Action<object, string>? LazyLoader { get; }

    public int Number { get; set; }

    [StringLength(256)] public string Name { get; set; } = default!;

    [StringLength(512)] public string? Description { get; set; }

    [StringLength(20)]
    [ForeignKey(nameof(Comic))]
    public ulong ComicId { get; set; }

    public int Views { get; set; }

    [DeleteBehavior(DeleteBehavior.Cascade)]
    public Comic Comic
    {
        get => LazyLoader.Load(this, ref _comic);
        set => _comic = value;
    }

    [InverseProperty(nameof(Page.Chapter))]
    public IList<Page> Pages
    {
        get => LazyLoader.Load(this, ref _pages).OrderBy(page => page.Number).ToList();
        set => _pages = value;
    }

    #endregion

    #region Constructor

    public Chapter()
    {
    }

    public Chapter(Action<object, string> lazyLoader)
    {
        LazyLoader = lazyLoader;
    }

    #endregion
}