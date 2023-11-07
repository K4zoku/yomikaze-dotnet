using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Yomikaze.Application.Data.Hubs;
using Yomikaze.Application.Data.Models;
using Yomikaze.Domain.Common;
using Yomikaze.Domain.Database.Entities;
using Yomikaze.Infrastructure.Data;

namespace Yomikaze.Application.Data.Access;

public class CommentDao : BaseDao<Comment>, IDao<Comment>
{
    private IHubContext<YomikazeHub> Hub { get; }

    public CommentDao(YomikazeDbContext dbContext, IHubContext<YomikazeHub> hub) : base(dbContext)
    {
        Hub = hub;
    }

    public override async Task AddAsync(Comment entity)
    {
        await base.AddAsync(entity);
        await Hub.Clients.All.SendAsync(nameof(Comment), "Added", entity.ToModel());
    }

    public override async Task<Comment> DeleteAsync(Comment entity)
    {
        var result = await base.DeleteAsync(entity);
        await Hub.Clients.All.SendAsync(nameof(Comment), "Deleted", entity.ToModel());
        return result;
    }
    public override async Task<Comment> UpdateAsync(Comment entity)
    {
        var result = await base.UpdateAsync(entity);
        await Hub.Clients.All.SendAsync(nameof(Comment), "Updated", entity);
        return result;

    }

    public override async Task<Comment?> GetAsync(long id)
    {
        return await DbSet
            .Include(comment => comment.User)
            .Include(comment => comment.Comic)
            .Include(comment => comment.Replies)
            .FirstOrDefaultAsync(entity => entity.Id.Equals(id));
    }

    public override async Task<IEnumerable<Comment>> GetAllAsync()
    {
        return await DbSet
            .Include(comment => comment.User)
            .Include(comment => comment.Comic)
            .Include(comment => comment.Replies)
            .ToListAsync();
    }
}
