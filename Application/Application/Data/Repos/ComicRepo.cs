using Microsoft.EntityFrameworkCore;
using Yomikaze.Application.Data.Access;
using Yomikaze.Domain.Abstracts;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Application.Data.Repos;

public class ComicRepo(DbContext dbContext) : BaseRepo<Comic>(new ComicDao(dbContext))
{
    public Comic? GetChaptersByComicId(ulong comicId)
    {
        return Query()
            .Include(comic => comic.Chapters)
            .FirstOrDefault(comic => comic.Id == comicId);
    }
}