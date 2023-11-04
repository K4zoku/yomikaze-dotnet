using Yomikaze.Domain.Common;
using Yomikaze.Domain.Database.Entities.Identity;

namespace Yomikaze.Domain.Database.Entities;
public class LibraryEntry : BaseEntity, IEntity
{
    public virtual Comic Comic { get; set; } = default!;

    public virtual User User { get; set; } = default!;

    public DateTimeOffset Added { get; set; }
}
