using Abstracts;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yomikaze.Domain.Entities;

public class Comic : BaseEntity
{
    public required string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? Cover { get; set; }
    public string? Banner { get; set; }
    public DateTimeOffset? Published { get; set; }
    public DateTimeOffset? Ended { get; set; }

    public virtual string? Aliases { get; set; } = default!;
    public virtual string? Authors { get; set; } = default!;
    
    [InverseProperty(nameof(ComicGenre.Comic))]
    public virtual ICollection<ComicGenre> ComicGenres { get; set; } = new List<ComicGenre>();
    

    [DeleteBehavior(DeleteBehavior.Cascade)]
    public virtual ICollection<Chapter> Chapters { get; set; } = new List<Chapter>();
}