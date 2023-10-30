using Yomikaze.Domain.Common;
using Yomikaze.Domain.Database.Entities;
using Yomikaze.Domain.Database.Entities.Identity;

namespace Yomikaze.WebAPI.Services;

public class CommentService
{
    private readonly IDao<Comment> _commentDao;

    public CommentService(IDao<Comment> commentDao)
    {
        _commentDao = commentDao;
    }


    public async Task<Comment> CreateCommentAsync(Comic comic, YomikazeUser yomikazeUser, string content)
    {
        var comment = new Comment
        {
            Comic = comic,
            User = yomikazeUser,
            Content = content,
        };
        await _commentDao.AddAsync(comment);
        return comment;
    }

    public async Task<Comment> UpdateCommentAsync(Comment comment, string content)
    {
        comment.Content = content;
        return await _commentDao.UpdateAsync(comment);
    }

    public async Task<Comment> DeleteCommentAsync(Comment comment)
    {
        return await _commentDao.DeleteAsync(comment);
    }

    public async Task<Comment?> GetCommentAsync(long id)
    {
        return await _commentDao.GetAsync(id);
    }

    public async Task<IEnumerable<Comment>> GetCommentsAsync(Comic comic)
    {
        return await _commentDao.FindAsync(c => c.Comic == comic);
    }

    //reply comment
    public async Task<Comment> CreateReplyCommentAsync(Comment comment, YomikazeUser yomikazeUser, string content)
    {
        var replyComment = new Comment
        {
            Comic = comment.Comic,
            User = yomikazeUser,
            Content = content,
            ReplyTo = comment,
        };
        await _commentDao.AddAsync(replyComment);
        return replyComment;
    }

}
