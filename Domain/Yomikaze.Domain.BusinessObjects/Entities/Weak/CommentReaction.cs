namespace Yomikaze.Domain.Entities.Weak;

[PrimaryKey(nameof(CommentId), nameof(UserId))]
public class CommentReaction
{
    [ForeignKey(nameof(Comment))] public ulong CommentId { get; init; }

    public Comment Comment { get; init; } = default!;

    [ForeignKey(nameof(User))] public ulong UserId { get; init; }

    public User User { get; init; } = default!;

    public ReactionType ReactionType { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is not CommentReaction other)
        {
            return false;
        }
        return UserId == other.UserId && CommentId == other.CommentId;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(UserId, CommentId);
    }
}

public enum ReactionType
{
    Like = 0,
    Dislike = 1
}