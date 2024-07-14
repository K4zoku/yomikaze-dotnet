namespace Yomikaze.Application.Data.Access;

public class ComicDao(DbContext dbContext) : BaseDao<Comic>(dbContext)
{
}