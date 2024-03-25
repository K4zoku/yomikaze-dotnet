using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Yomikaze.Domain.Abstracts;

namespace Yomikaze.Domain.Entities;

public class ComicGenre : BaseEntity
{
    [ForeignKey(nameof(Comic))] public string ComicId { get; set; }

    [DeleteBehavior(DeleteBehavior.Cascade)]
    public virtual Comic Comic { get; set; } = default!;

    [ForeignKey(nameof(Genre))] public string GenreId { get; set; }

    [DeleteBehavior(DeleteBehavior.Cascade)]
    public virtual Genre Genre { get; set; } = default!;
}