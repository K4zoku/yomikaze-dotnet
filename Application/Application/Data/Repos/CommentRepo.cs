using Abstracts;
using Microsoft.EntityFrameworkCore;
using Yomikaze.Application.Data.Access;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Application.Data.Repos;

public class CommentRepo(DbContext dbContext) : BaseRepo<Comment>(new CommentDao(dbContext))
{
    public IEnumerable<Comment> GetCommentByComicId(long comicId)
    {
        return Query().Where(comment => comment.ComicId == comicId);
    }
}