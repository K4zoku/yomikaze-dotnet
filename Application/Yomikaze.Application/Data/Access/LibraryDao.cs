namespace Yomikaze.Application.Data.Access;

public class LibraryDao(DbContext dbContext) : BaseDao<LibraryEntry>(dbContext)
{
}