using Abstracts;
using Microsoft.EntityFrameworkCore;
using Yomikaze.Application.Data.Access;
using Yomikaze.Domain.Entities;
using Yomikaze.Domain.Models;

namespace Yomikaze.Application.Data.Repos;

public class ComicRepo(DbContext dbContext) : BaseRepo<Comic>(new ComicDao(dbContext))
{
    public Comic? GetChaptersByComicId(long comicId)
    {
        return Query()
            .Include(comic => comic.Chapters)
            .FirstOrDefault(comic => comic.Id == comicId);
    }

}