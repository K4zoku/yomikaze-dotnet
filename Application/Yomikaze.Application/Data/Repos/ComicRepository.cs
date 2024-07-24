using Yomikaze.Application.Data.Access;
using Yomikaze.Infrastructure.Context;

namespace Yomikaze.Application.Data.Repos;

public class ComicRepository(YomikazeDbContext dbContext) : BaseRepository<Comic>(new ComicDao(dbContext))
{
    private YomikazeDbContext DbContext { get; } = dbContext;
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
        return base.Query()
            .Where(comic => comic.Id == id)
            .Include(comic => comic.Chapters)
            .Include(comic => comic.Ratings)
            .Include(comic => comic.Follows)
            .Include(comic => comic.Comments)
            .AsSingleQuery()
            .FirstOrDefault();
    }
    
    public Comic? GetWithExtras(ulong id)
    {
        return base.Query()
            .Where(comic => comic.Id == id)
            .Include(comic => comic.Tags)
            .Include(comic => comic.Publisher)
            .Include(comic => comic.Chapters)
            .Include(comic => comic.Ratings)
            .Include(comic => comic.Follows)
            .Include(comic => comic.Comments)
            .AsSingleQuery()
            .FirstOrDefault();
    }
    
    public Comic? GetRandomComic()
    {
        return DbContext.GetRandomComic().FirstOrDefault();
    }
}