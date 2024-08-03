using Yomikaze.Application.Data.Access;

namespace Yomikaze.Application.Data.Repos;

public class LibraryRepository(DbContext dbContext) : BaseRepository<LibraryEntry>(new LibraryDao(dbContext))
{
    public LibraryEntry? GetLibraryEntry(ulong userId, ulong comicId)
    {
        return Query()
            .FirstOrDefault(x => x.UserId == userId && x.ComicId == comicId);
    }

    public IQueryable<LibraryEntry> GetByCategory(string userId, ulong categoryId)
    {
        return Query().Where(x => x.UserId.ToString() == userId && x.Categories.Any(c => c.Id == categoryId));
    }

    public bool IsFollowing(ulong userId, ulong comicId)
    {
        return Query().Any(x => x.UserId == userId && x.ComicId == comicId);
    }

    public IQueryable<LibraryEntry> GetAllByUserId(string userId)
    {
        return Query().Where(x => x.UserId.ToString() == userId);
    }
}