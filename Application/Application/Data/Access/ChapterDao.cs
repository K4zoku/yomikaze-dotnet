using Microsoft.EntityFrameworkCore;
using Yomikaze.Domain.Abstracts;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Application.Data.Access;

public class ChapterDao(DbContext dbContext) : BaseDao<Chapter>(dbContext)
{
    public override IQueryable<Chapter> Query()
    {
        return base.Query()
            .Include(c => c.Comic)
            .Include(c => c.Pages);
    }
}