using Abstracts;
using Microsoft.EntityFrameworkCore;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Application.Data.Access;

public class ComicDao(DbContext dbContext) : BaseDao<Comic>(dbContext)
{
    public override IQueryable<Comic> Query()
    {
        return base.Query()
            .Include(entity => entity.ComicGenres)
            .ThenInclude(entity => entity.Genre)
            ;
    }
}