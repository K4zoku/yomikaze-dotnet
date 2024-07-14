namespace Yomikaze.Application.Data.Access;

public class ChapterDao(DbContext dbContext) : BaseDao<Chapter>(dbContext)
{
}