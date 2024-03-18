using Microsoft.EntityFrameworkCore;
using Yomikaze.Domain.Abstracts;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Application.Data.Access;

public class GenreDao(DbContext dbContext) : BaseDao<Genre>(dbContext)
{
    public override IQueryable<Genre> Query()
    {
        return base.Query()
            .Include(genre => genre.ComicGenres)
            .ThenInclude(comicGenre => comicGenre.Comic);
    }
}