using Microsoft.EntityFrameworkCore;
using Yomikaze.Application.Data.Access;
using Yomikaze.Domain.Abstracts;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Application.Data.Repos;

public class CommentRepository(DbContext dbContext) : BaseRepository<Comment>(new CommentDao(dbContext))
{
    public IEnumerable<Comment> GetCommentByComicId(string comicId)
    {
        return Query().Where(comment => comment.ComicId == comicId);
    }
}