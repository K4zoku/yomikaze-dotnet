namespace Yomikaze.Domain.Entities.Weak;

[PrimaryKey(nameof(UserId), nameof(ChapterId))]
public class UnlockedChapter
{
    [ForeignKey(nameof(User))] public ulong UserId { get; init; }
    
    public User User { get; init; } = default!;

    [ForeignKey(nameof(Chapter))] public ulong ChapterId { get; init; }
    
    public Chapter Chapter { get; init; } = default!;
    
    public override bool Equals(object? obj)
    {
        if (obj is not UnlockedChapter other)
        {
            return false;
        }

        return UserId == other.UserId && ChapterId == other.ChapterId;
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(UserId, ChapterId);
    }
}