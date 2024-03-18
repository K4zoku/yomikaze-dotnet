using System.ComponentModel.DataAnnotations.Schema;
using Yomikaze.Domain.Abstracts;

namespace Yomikaze.Domain.Entities;

public class LibraryEntry : BaseEntity
{
    [ForeignKey(nameof(Comic))] public ulong ComicId { get; set; }

    public virtual Comic Comic { get; set; } = default!;

    [ForeignKey(nameof(User))] public ulong UserId { get; set; }

    public DateTimeOffset DateAdded { get; set; } = DateTimeOffset.Now;

    public virtual User User { get; set; } = default!;
}