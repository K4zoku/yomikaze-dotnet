using Microsoft.EntityFrameworkCore;
using Yomikaze.Domain.Common;
using Yomikaze.Domain.Database.Entities;
using Yomikaze.Infrastructure.Data;

namespace Yomikaze.Application.Data.Access;

public class ChapterDao : BaseDao<Chapter>, IDao<Chapter>
{

    public ChapterDao(YomikazeDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<IEnumerable<Chapter>> GetAllAsync()
    {
        return await DbSet.Include(c => c.Comic).Include(c => c.Pages).ToListAsync();
    }

    public override async Task<Chapter?> GetAsync(long id)
    {
        return await DbSet.Include(c => c.Comic).Include(c => c.Pages).FirstOrDefaultAsync(c => c.Id == id);
    }

}
