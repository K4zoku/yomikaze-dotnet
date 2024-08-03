using Yomikaze.Domain.Entities.Weak;

namespace Yomikaze.Domain.Entities;

[Index(nameof(ComicId), nameof(UserId), IsUnique = true)]
public class LibraryEntry : BaseEntity
{
    #region Properties

    [ForeignKey(nameof(Comic))]
    [DataMember(Name = "comicId", Order = 1)]
    [Column("comic_id", Order = 1)]
    public ulong ComicId { get; set; }

    [DataMember(Name = "comic")]
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public Comic Comic { get; set; } = default!;

    [ForeignKey(nameof(User))]
    [DataMember(Name = "userId", Order = 2)]
    [Column("user_id", Order = 2)]
    public ulong UserId { get; set; }

    [DeleteBehavior(DeleteBehavior.Cascade)]
    public User User { get; set; } = default!;

    public IList<LibraryEntryCategory> LibraryCategories { get; set; } = new List<LibraryEntryCategory>();

    [DeleteBehavior(DeleteBehavior.Cascade)]
    public List<LibraryCategory> Categories { get; set; } = new();

    #endregion
}