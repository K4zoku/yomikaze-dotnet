using Yomikaze.Application.Data.Access;

namespace Yomikaze.Application.Data.Repos;

public class ComicCommentRepository(DbContext dbContext) : BaseRepository<ComicComment>(new ComicCommentDao(dbContext)) 
{
    public IQueryable<ComicComment> GetByComicId(ulong comicId)
    {
        return Query().Where(x => x.ComicId == comicId);
    }
    
    public IQueryable<ComicComment> GetRepliesByCommentId(ulong commentId)
    {
        return Query().Where(x => x.ReplyToId == commentId);
    }
}