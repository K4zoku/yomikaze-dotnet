namespace Yomikaze.Domain.Entities;

public class ComicComment : Comment
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

    #endregion

    #region Constructors

    public ComicComment()
    {
    }

    public ComicComment(Action<object, string> lazyLoader) : base(lazyLoader)
    {
    }

    #endregion
}