using Microsoft.EntityFrameworkCore;
using Yomikaze.Domain.Common;
using Yomikaze.Domain.Database.Entities;
using Yomikaze.Infrastructure.Data;

namespace Yomikaze.Application.Data.Access;

public class GenreDao : BaseDao<Genre>, IDao<Genre>
{

    public GenreDao(YomikazeDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<IEnumerable<Genre>> GetAllAsync()
    {
        return await DbSet.Include(g => g.Comics).ToListAsync();
    }

    public override async Task<Genre?> GetAsync(long id)
    {
        return await DbSet.Include(g => g.Comics).FirstOrDefaultAsync(g => g.Id == id);
    }

}
