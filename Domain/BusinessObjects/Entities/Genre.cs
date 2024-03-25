using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Yomikaze.Domain.Abstracts;

namespace Yomikaze.Domain.Entities;

[Index(nameof(Name), IsUnique = true)]
public class Genre : BaseEntity
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }

    [InverseProperty(nameof(ComicGenre.Genre))]
    public virtual ICollection<ComicGenre> ComicGenres { get; private set; } = new List<ComicGenre>();
}