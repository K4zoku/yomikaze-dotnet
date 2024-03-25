using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;
using Yomikaze.Domain.Abstracts;

namespace Yomikaze.Domain.Entities;

public class Comic : BaseEntity
{
    private Action<object, string> LazyLoader { get; set; }
    public Comic(Action<object, string> lazyLoader)
    {
        LazyLoader = lazyLoader;
    }
    
    public Comic()
    {
        
    }
    
    public required string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? Cover { get; set; }
    public string? Banner { get; set; }
    public DateTimeOffset? Published { get; set; }
    public DateTimeOffset? Ended { get; set; }

    public virtual string? Aliases { get; set; } = default!;
    public virtual string? Authors { get; set; } = default!;

    private ICollection<ComicGenre> _comicGenres = new List<ComicGenre>();
    [InverseProperty(nameof(ComicGenre.Comic))]
    public virtual ICollection<ComicGenre> ComicGenres { 
        get => LazyLoader.Load(this, ref _comicGenres);
        set => _comicGenres = value;
    }


    private IList<Chapter> _chapters = new List<Chapter>();
    [DeleteBehavior(DeleteBehavior.Cascade)]
    [InverseProperty(nameof(Chapter.Comic))]
    public virtual IList<Chapter> Chapters { 
        get => LazyLoader.Load(this, ref _chapters);
        set => _chapters = value;
    }
}