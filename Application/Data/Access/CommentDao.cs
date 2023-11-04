using Microsoft.AspNetCore.SignalR;
using Yomikaze.Application.Data.Hubs;
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
        await Hub.Clients.All.SendAsync(nameof(Comment), "Added", entity.Id);
    }

    public override async Task<Comment> DeleteAsync(Comment entity)
    {
        var result = await base.DeleteAsync(entity);
        await Hub.Clients.All.SendAsync(nameof(Comment), "Deleted", entity.Id);
        return result;
    }
    public override async Task<Comment> UpdateAsync(Comment entity)
    {
        var result = await base.UpdateAsync(entity);
        await Hub.Clients.All.SendAsync(nameof(Comment), "Updated", entity.Id);
        return result;

    }
}
