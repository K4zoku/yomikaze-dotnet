using Microsoft.EntityFrameworkCore;
using Yomikaze.Application.Data.Access;
using Yomikaze.Domain.Abstracts;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Application.Data.Repos;

public class LibraryRepo(DbContext dbContext) : BaseRepo<LibraryEntry>(new LibraryDao(dbContext))
{
    public IEnumerable<LibraryEntry> GetLibraryByUserId(ulong userId)
    {
        return Query().Where(x => x.UserId == userId);
    }

    public LibraryEntry? GetLibraryEntry(ulong userId, ulong comicId)
    {
        return Query().FirstOrDefault(x => x.UserId == userId && x.ComicId == comicId);
    }
}