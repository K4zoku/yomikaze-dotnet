namespace Yomikaze.Domain.Entities.Weak;

[PrimaryKey(nameof(ComicId), nameof(UserId))]
public class ComicRating
{
    [ForeignKey(nameof(Comic))] public ulong ComicId { get; init; }

    public Comic Comic { get; init; } = default!;

    [ForeignKey(nameof(User))] public ulong UserId { get; init; }

    public User User { get; init; } = default!;

    [Range(1, 5)] public int Rating { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is not ComicRating other)
        {
            return false;
        }

        return ComicId == other.ComicId && UserId == other.UserId;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(ComicId, UserId);
    }
}