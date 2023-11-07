using System.ComponentModel.DataAnnotations.Schema;
using Yomikaze.Domain.Common;
using Yomikaze.Domain.Database.Entities.Identity;

namespace Yomikaze.Domain.Database.Entities;
public class LibraryEntry : BaseEntity, IEntity
{
    [ForeignKey(nameof(Comic))]
    public long ComicId { get; set; }
    public virtual Comic Comic { get; set; } = default!;

    [ForeignKey(nameof(User))]
    public long UserId { get; set; }
    public virtual User User { get; set; } = default!;

    public DateTimeOffset Added { get; set; }
}
