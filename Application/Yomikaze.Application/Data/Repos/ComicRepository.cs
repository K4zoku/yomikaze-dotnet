using Yomikaze.Application.Data.Access;

namespace Yomikaze.Application.Data.Repos;

public class ComicRepository(DbContext dbContext) : BaseRepository<Comic>(new ComicDao(dbContext))
{
    public override IQueryable<Comic> Query()
    {
        return base.Query()
            .Include(comic => comic.Tags)
            .Include(comic => comic.Publisher)
            .AsSingleQuery();
    }
    
    public IQueryable<Comic> QueryWithExtras()
    {
        return base.Query()
            .Include(comic => comic.Tags)
            .Include(comic => comic.Publisher)
            .Include(comic => comic.Chapters)
            .Include(comic => comic.Ratings)
            .Include(comic => comic.Follows)
            .Include(comic => comic.Comments)
            .AsSingleQuery();
    }

    public override Comic? Get(ulong id)
    {
        return Query()
            .Include(comic => comic.Chapters)
            .Include(comic => comic.Ratings)
            .Include(comic => comic.Follows)
            .Include(comic => comic.Comments)
            .AsSingleQuery()
            .FirstOrDefault(comic => comic.Id == id);
    }

    public Comic? GetChaptersByComicId(string comicId)
    {
        return Query().FirstOrDefault(comic => comic.Id.ToString() == comicId);
    }

    public Comic? GetChaptersByComicId(ulong comicId)
    {
        return Query().FirstOrDefault(comic => comic.Id == comicId);
    }
}