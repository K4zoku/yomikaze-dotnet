using System.Linq.Dynamic.Core;
using Yomikaze.Application.Data.Access;

namespace Yomikaze.Application.Data.Repos;

public class ChapterRepository(DbContext dbContext) : BaseRepository<Chapter>(new ChapterDao(dbContext))
{
    public Chapter? Get(string id)
    {
        return Query().FirstOrDefault(chapter => chapter.Id.ToString() == id);
    }

    public Chapter? GetByComicIdAndIndex(string comicId, int index)
    {
        return Query()
            .Include(chapter => chapter.Pages)
            .Include(chapter => chapter.Unlocked)
            .FirstOrDefault(chapter => chapter.ComicId.ToString() == comicId && chapter.Number == index);
    }

    public ICollection<Chapter> GetByComicIdAndIndexes(string comicId, params int[] indexes)
    {
        return Query()
            .Include(chapter => chapter.Pages)
            .Include(chapter => chapter.Unlocked)
            .Where(chapter => chapter.ComicId.ToString() == comicId && indexes.Contains(chapter.Number))
            .ToList();
    }
}