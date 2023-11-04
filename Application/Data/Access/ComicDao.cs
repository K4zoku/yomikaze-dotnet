using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Yomikaze.Application.Data.Hubs;
using Yomikaze.Domain.Common;
using Yomikaze.Domain.Database.Entities;
using Yomikaze.Infrastructure.Data;

namespace Yomikaze.Application.Data.Access;

public class ComicDao : BaseDao<Comic>, IDao<Comic>
{
    private IHubContext<YomikazeHub> Hub { get; }
    public ComicDao(YomikazeDbContext dbContext, IHubContext<YomikazeHub> hub) : base(dbContext)
    {
        Hub = hub;
    }

    public override async Task AddAsync(Comic entity)
    {
        await base.AddAsync(entity);
        await Hub.Clients.All.SendAsync("Comic", "Added", entity.Id);
    }

    public override async Task<Comic> DeleteAsync(Comic entity)
    {
        var result = await base.DeleteAsync(entity);
        await Hub.Clients.All.SendAsync("Comic", "Deleted", entity.Id);
        return result;
    }

    public override async Task<Comic?> GetAsync(long id)
    {
        return await DbSet
            .Where(entity => entity.Id.Equals(id))
            .Include(entity => entity.Genres)
            .Include(entity => entity.Chapters)
            //.ThenInclude(entity => entity.Pages)
            .FirstOrDefaultAsync();
    }

    public override async Task<IEnumerable<Comic>> GetAllAsync()
    {
        return await DbSet
            .Include(entity => entity.Genres)
            //.Include(entity => entity.Chapters)
            //.ThenInclude(entity => entity.Pages)
            .ToListAsync();
    }

    public override async Task<Comic> UpdateAsync(Comic entity)
    {
        var result = await base.UpdateAsync(entity);
        await Hub.Clients.All.SendAsync("Comic", "Updated", entity.Id);
        return result;

    }
}