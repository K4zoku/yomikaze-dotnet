using Yomikaze.Domain.Entities.Weak;

namespace Yomikaze.Domain.Entities;

public class Comment : BaseEntity
{
    #region Fields

    private User _author = default!;
    private Comment? _replyTo;
    private ICollection<Comment> _replies = [];

    #endregion

    #region Properties

    protected Action<object, string>? LazyLoader { get; }

    [StringLength(1024)] public string Content { get; set; } = default!;


    [ForeignKey(nameof(Author))] public ulong AuthorId { get; set; }

    public User Author
    {
        get => LazyLoader.Load(this, ref _author);
        set => _author = value;
    }

    [ForeignKey(nameof(ReplyTo))] public ulong? ReplyToId { get; set; }

    [DeleteBehavior(DeleteBehavior.Cascade)]
    public Comment? ReplyTo
    {
        get => LazyLoader.LoadNullable(this, ref _replyTo);
        set => _replyTo = value;
    }

    [DataMember(Name = "replies")]
    [InverseProperty(nameof(ReplyTo))]
    public ICollection<Comment> Replies
    {
        get => LazyLoader.Load(this, ref _replies);
        set => _replies = value;
    }
    
    public ICollection<CommentReaction> Reactions { get; set; } = [];

    #endregion

    #region Constructors

    protected Comment()
    {
    }

    protected Comment(Action<object, string> lazyLoader)
    {
        LazyLoader = lazyLoader;
    }

    #endregion
}