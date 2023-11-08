using Microsoft.EntityFrameworkCore;
using Yomikaze.Domain.Common;
using Yomikaze.Domain.Database.Entities;
using Yomikaze.Infrastructure.Data;

namespace Yomikaze.Application.Data.Access;

public class PageDao : BaseDao<Page>, IDao<Page>
{

    public PageDao(YomikazeDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<IEnumerable<Page>> GetAllAsync()
    {
        return await DbSet.Include(p => p.Chapter).ToListAsync();
    }

    public override async Task<Page?> GetAsync(long id)
    {
        return await DbSet.Include(p => p.Chapter).FirstOrDefaultAsync(p => p.Id == id);
    }

}
