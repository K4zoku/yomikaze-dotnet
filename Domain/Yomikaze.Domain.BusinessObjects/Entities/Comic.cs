using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Yomikaze.Domain.Abstracts;
using Yomikaze.Domain.Entities.Weak;

namespace Yomikaze.Domain.Entities;

[Table("comics")]
[DataContract(Name = "comic")]
public class Comic : BaseEntity
{

    #region Fields

    private ICollection<Genre> _genres = [];
    private ICollection<ComicGenre> _comicGenres = [];
    private ICollection<Author> _authors = [];
    private ICollection<ComicAuthor> _comicAuthors = [];
    private ICollection<Chapter> _chapters = [];

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
    public ICollection<Genre> Genres => LazyLoader.Load(this, ref _genres);
    
    public ICollection<ComicGenre> ComicGenres {
        get => LazyLoader.Load(this, ref _comicGenres);
        set => _comicGenres = value;
    }

    [DataMember(Name = "authors")]
    public ICollection<Author> Authors => LazyLoader.Load(this, ref _authors);
    
    public ICollection<ComicAuthor> ComicAuthors {
        get => LazyLoader.Load(this, ref _comicAuthors);
        set => _comicAuthors = value;
    }

    [DataMember(Name = "chapters")]
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public ICollection<Chapter> Chapters
    {
        get => LazyLoader.Load(this, ref _chapters);
        set => _chapters = value;
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