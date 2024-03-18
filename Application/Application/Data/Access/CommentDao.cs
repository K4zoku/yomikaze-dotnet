using Abstracts;
using Microsoft.EntityFrameworkCore;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Application.Data.Access;

public class CommentDao(DbContext dbContext) : BaseDao<Comment>(dbContext)
{
    public override IQueryable<Comment> Query()
    {
        return base.Query()
            .Include(comment => comment.User)
            // .Include(comment => comment.Comic)
            .Include(comment => comment.Replies);
    }

    public IEnumerable<Comment> GetCommentByComicId(long comicId)
    {
        return  Query().Where(comment => comment.ComicId == comicId);
    }
}