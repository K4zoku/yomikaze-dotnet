using Abstracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yomikaze.Domain.Entities;

public class LibraryEntry : BaseEntity
{
    [ForeignKey(nameof(Comic))] public long ComicId { get; set; }

    public virtual Comic Comic { get; set; } = default!;

    [ForeignKey(nameof(User))] public long UserId { get; set; }

    public DateTimeOffset DateAdded { get; set; } = DateTimeOffset.Now;

    public virtual User User { get; set; } = default!;
}