using Yomikaze.Domain.Common;
using Yomikaze.Domain.Database.Entities;

namespace Yomikaze.WebAPI.Services;

public class LibraryService
{
    private readonly IDao<LibraryEntry> _libraryDao;

    public LibraryService(IDao<LibraryEntry> libraryDao)
    {
        _libraryDao = libraryDao;
    }

    //public async Task<LibraryEntry> AddToLibraryAsync(long comicId, ClaimsPrincipal principal)
    //{
    //    var userId = principal.GetId();
    //    var entry = new LibraryEntry
    //    {
    //        ComicId = comicId,
    //        UserId = userId
    //    };
    //    await _libraryDao.AddAsync(entry);
    //    _libraryDao.SaveChanges();
    //    return entry;
    //}
}
