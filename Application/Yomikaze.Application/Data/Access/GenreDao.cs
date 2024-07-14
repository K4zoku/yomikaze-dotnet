namespace Yomikaze.Application.Data.Access;

public class GenreDao(DbContext dbContext) : BaseDao<Tag>(dbContext)
{
}