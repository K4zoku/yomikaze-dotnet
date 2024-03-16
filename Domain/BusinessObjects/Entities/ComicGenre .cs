using Abstracts;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yomikaze.Domain.Entities;
public class ComicGenre : BaseEntity
{
    [ForeignKey(nameof(Comic))]
    public long ComicId { get; set; }
    
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public virtual Comic Comic { get; set; } = default!;

    [ForeignKey(nameof(Genre))]
    public long GenreId { get; set; }
    
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public virtual Genre Genre { get; set; } = default!;
}
