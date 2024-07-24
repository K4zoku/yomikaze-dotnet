using Yomikaze.Application.Data.Access;

namespace Yomikaze.Application.Data.Repos;

public class ChapterCommentRepository(DbContext dbContext) : BaseRepository<ChapterComment>(new ChapterCommentDao(dbContext)) 
{
    public override IQueryable<ChapterComment> Query()
    {
        return base.Query().Include(x => x.Author).ThenInclude(x => x.Roles);
    }

    public IQueryable<ChapterComment> GetByComicAndChapter(ulong comicId, int chapterNumber)
    {
        return Query()
            .Include(x => x.Chapter)
            .ThenInclude(x => x.Comic)
            .Where(x => x.Chapter.ComicId == comicId && x.Chapter.Number == chapterNumber);
    }
    
    public IQueryable<ChapterComment> GetRepliesByCommentId(ulong commentId)
    {
        return Query().Where(x => x.ReplyToId == commentId);
    }
}