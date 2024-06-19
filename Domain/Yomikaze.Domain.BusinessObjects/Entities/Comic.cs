using Yomikaze.Domain.Entities.Weak;

namespace Yomikaze.Domain.Entities;

public sealed class Comic : BaseEntity
{
    #region Fields

    private ICollection<Tag> _tags = [];
    private ICollection<ComicTag> _comicTags = [];
    private User? _publisher;

    #endregion

    #region Properties

    private Action<object, string>? LazyLoader { get; }

    [StringLength(256)] public string Name { get; set; } = default!;

    public string[] Aliases { get; set; } = [];

    [StringLength(512)] public string? Description { get; set; }

    [StringLength(512)] public string? Cover { get; set; }

    [StringLength(512)] public string? Banner { get; set; }

    public DateTimeOffset? PublicationDate { get; set; }

    public ICollection<Tag> Tags
    {
        get => LazyLoader.Load(this, ref _tags);
        set => _tags = value;
    }

    public ICollection<ComicTag> ComicTags
    {
        get => LazyLoader.Load(this, ref _comicTags);
        set => _comicTags = value;
    }

    public string[] Authors { get; set; } = [];

    [ForeignKey(nameof(Publisher))] public ulong? PublisherId { get; set; }

    [DeleteBehavior(DeleteBehavior.Cascade)]
    public User? Publisher
    {
        get => LazyLoader.LoadNullable(this, ref _publisher);
        set => _publisher = value;
    }

    public ComicStatus Status { get; set; }

    #endregion

    #region Constructors

    [NotMapped]
    public override int WorkerId => 3;
    public Comic()
    {
    }

    public Comic(Action<object, string> lazyLoader) : this()
    {
        LazyLoader = lazyLoader;
    }

    #endregion
}

public enum ComicStatus
{
    OnGoing = 0,
    Completed = 1,
    Hiatus = 2,
    Cancelled = 3
}