namespace Yomikaze.Domain.Entities.Weak;

[PrimaryKey(nameof(UserId), nameof(ChapterId))]
public class UnlockedChapter
{
    [ForeignKey(nameof(User))] public ulong UserId { get; set; }

    [ForeignKey(nameof(Chapter))] public ulong ChapterId { get; set; }
}