using Yomikaze.Application.Data.Access;

namespace Yomikaze.Application.Data.Repos;

public class ComicCommentRepository(DbContext dbContext) : BaseRepository<ComicComment>(new ComicCommentDao(dbContext)) 
{
    public override IQueryable<ComicComment> Query()
    {
        return base.Query().Include(x => x.Author).ThenInclude(x => x.Roles);
    }

    public IQueryable<ComicComment> GetByComicId(ulong comicId)
    {
        return Query().Where(x => x.ComicId == comicId);
    }
    
    public IQueryable<ComicComment> GetRepliesByCommentId(ulong commentId)
    {
        return Query().Where(x => x.ReplyToId == commentId);
    }
}