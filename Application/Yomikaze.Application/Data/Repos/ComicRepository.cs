using Microsoft.EntityFrameworkCore;
using Yomikaze.Application.Data.Access;
using Yomikaze.Domain.Abstracts;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Application.Data.Repos;

public class ComicRepository(DbContext dbContext) : BaseRepository<Comic>(new ComicDao(dbContext))
{
    public Comic? GetChaptersByComicId(string comicId)
    {
        return Query().FirstOrDefault(comic => comic.Id.ToString() == comicId);
    }

    public Comic? GetChaptersByComicId(ulong comicId)
    {
        return Query().FirstOrDefault(comic => comic.Id == comicId);
    }
}