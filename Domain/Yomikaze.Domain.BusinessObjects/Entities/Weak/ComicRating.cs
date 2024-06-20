namespace Yomikaze.Domain.Entities.Weak;

[PrimaryKey(nameof(ComicId), nameof(UserId))]
public class ComicRating
{
    [ForeignKey(nameof(Comic))] public ulong ComicId { get; set; }

    public Comic Comic { get; set; } = default!;

    [ForeignKey(nameof(User))] public ulong UserId { get; set; }

    public User User { get; set; } = default!;

    [Range(1, 5)] public int Rating { get; set; }
}