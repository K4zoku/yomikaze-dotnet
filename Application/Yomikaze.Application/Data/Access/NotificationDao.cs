namespace Yomikaze.Application.Data.Access;

public class NotificationDao(DbContext dbContext) : BaseDao<Notification>(dbContext)
{
}