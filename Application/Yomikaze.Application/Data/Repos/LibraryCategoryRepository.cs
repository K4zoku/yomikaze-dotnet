using Yomikaze.Application.Data.Access;

namespace Yomikaze.Application.Data.Repos;

public class LibraryCategoryRepository(DbContext dbContext)
    : BaseRepository<LibraryCategory>(new LibraryCategoryDao(dbContext))
{
    public LibraryCategory? GetByNameAndUserId(string name, ulong userId)
    {
        return Query().FirstOrDefault(x => x.Name == name && x.UserId == userId);
    }

    public IQueryable<LibraryCategory> GetAllByUserId(ulong userId)
    {
        return Query().Where(x => x.UserId == userId);
    }
}