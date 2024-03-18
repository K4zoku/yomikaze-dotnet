using Microsoft.EntityFrameworkCore;
using Yomikaze.Application.Data.Access;
using Yomikaze.Domain.Abstracts;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Application.Data.Repos;

public class ChapterRepo(DbContext dbContext) : BaseRepo<Chapter>(new ChapterDao(dbContext))
{
    public Chapter? GetByComicIdAndIndex(string comicId, int index)
    {
        return Query()
            .Include(chapter => chapter.Pages)
            .FirstOrDefault(chapter => chapter.ComicId == comicId && chapter.Index == index);
    }
}