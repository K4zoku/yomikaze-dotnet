using Yomikaze.Application.Data.Access;

namespace Yomikaze.Application.Data.Repos;

public class NotificationRepository(DbContext dbContext) : BaseRepository<Notification>(new NotificationDao(dbContext))
{
    public IQueryable<Notification> GetByUserId(ulong userId)
    {
        return Query().Where(x => x.UserId == userId);
    }
}