using Microsoft.EntityFrameworkCore;
using Yomikaze.Application.Data.Access;
using Yomikaze.Domain.Abstracts;
using Yomikaze.Domain.Entities;

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
            .FirstOrDefault(chapter => chapter.ComicId.ToString() == comicId && chapter.Number == index);
    }
}