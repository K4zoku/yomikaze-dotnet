using Yomikaze.Domain.Entities.Weak;

namespace Yomikaze.Domain.Entities;

[Table("comics")]
[DataContract(Name = "comic")]
public class Comic : BaseEntity
{

    #region Fields

    private ICollection<Tag> _genres = [];
    private ICollection<ComicTags> _comicGenres = [];
    private IEnumerable<Chapter> _chapters = [];
    private UserProfile? _publisher;

    #endregion

    #region Properties
    private Action<object, string>? LazyLoader { get; set; }

    [StringLength(256)]
    [DataMember(Name = "name")]
    [Column("name", Order = 1)]
    public string Name { get; set; } = default!;

    [DataMember(Name = "aliases")]
    [Column("aliases", Order = 2)]
    public ICollection<string> Aliases { get; set; } = [];

    [StringLength(512)]
    [DataMember(Name = "description")]
    [Column("description", Order = 3)]
    public string? Description { get; set; }

    [StringLength(512)]
    [DataMember(Name = "cover")]
    [Column("cover", Order = 4)]
    public string? Cover { get; set; }

    [StringLength(512)]
    [DataMember(Name = "banner")]
    [Column("banner", Order = 5)]
    public string? Banner { get; set; }

    [DataMember(Name = "published")]
    [Column("published", Order = 6)]
    public DateTimeOffset? Published { get; set; }

    [DataMember(Name = "ended")]
    [Column("ended", Order = 7)]
    public DateTimeOffset? Ended { get; set; }

    [DataMember(Name = "genres")]
    public ICollection<Tag> Genres => LazyLoader.Load(this, ref _genres);
    
    public ICollection<ComicTags> ComicGenres {
        get => LazyLoader.Load(this, ref _comicGenres);
        set => _comicGenres = value;
    }

    [DataMember(Name = "authors")]
    public ICollection<string> Authors { get; set; }

    [DataMember(Name = "chapters")]
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public IEnumerable<Chapter> Chapters
    {
        get => LazyLoader.Load(this, ref _chapters).OrderBy(x => x.Index);
        set => _chapters = value;
    }
    
    [ForeignKey(nameof(Publisher))]
    [DataMember(Name = "publisherId")]
    [Column("publisher_id", Order = 8)]
    public ulong? PublisherId { get; set; }
    
    [DeleteBehavior(DeleteBehavior.SetNull)]
    public UserProfile? Publisher { 
        get => LazyLoader.LoadNullable(this, ref _publisher);
        set => _publisher = value;
    }

    #endregion

    #region Constructors

    public Comic()
    {
    }

    public Comic(Action<object, string> lazyLoader)
    {
        LazyLoader = lazyLoader;
    }

    #endregion
}