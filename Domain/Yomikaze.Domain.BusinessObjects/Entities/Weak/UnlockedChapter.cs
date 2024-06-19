using Yomikaze.Domain.Identity.Entities;

namespace Yomikaze.Domain.Entities.Weak;

[Index(nameof(UserId), nameof(ChapterId), IsUnique = true)]
public class UnlockedChapter
{
    [ForeignKey(nameof(User))]
    public ulong UserId { get; set; }
    
    [ForeignKey(nameof(Chapter))]
    public ulong ChapterId { get; set; }
}