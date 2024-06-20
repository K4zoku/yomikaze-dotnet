namespace Yomikaze.Domain.Entities.Weak;

[PrimaryKey(nameof(CommentId), nameof(UserId))]
public class CommentReaction
{
    [ForeignKey(nameof(Comment))] public ulong CommentId { get; set; }

    public Comment Comment { get; set; } = default!;

    [ForeignKey(nameof(User))] public ulong UserId { get; set; }

    public User User { get; set; } = default!;

    public ReactionType ReactionType { get; set; }
}

public enum ReactionType
{
    Like = 0,
    Dislike = 1
}