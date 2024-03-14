using Microsoft.EntityFrameworkCore;
using Yomikaze.Domain.Common;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Application.Data.Access;

public class ComicDao(DbContext dbContext) : BaseDao<Comic>(dbContext)
{
    public override IQueryable<Comic> Query()
    {
        return base.Query()
            .Include(entity => entity.Genres)
            .Include(entity => entity.Chapters)
            .ThenInclude(entity => entity.Pages);
    }
}