using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Yomikaze.Application.Data.Hubs;
using Yomikaze.Domain.Common;
using Yomikaze.Domain.Database.Entities;
using Yomikaze.Infrastructure.Data;

namespace Yomikaze.Application.Data.Access;

public class GenreDao : BaseDao<Genre>, IDao<Genre>
{
    private IHubContext<YomikazeHub> Hub { get; }

    public GenreDao(YomikazeDbContext dbContext, IHubContext<YomikazeHub> hub) : base(dbContext)
    {
        Hub = hub;
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
