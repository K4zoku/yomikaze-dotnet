using System.Security.Claims;
using Yomikaze.Application.Data.Models;
using Yomikaze.Application.Data.Models.Common;
using Yomikaze.Domain.Common;
using Yomikaze.Domain.Database.Entities;
using Yomikaze.WebAPI.Helpers;

namespace Yomikaze.WebAPI.Services;

public class LibraryService
{
    private readonly IDao<LibraryEntry> _libraryDao;

    public LibraryService(IDao<LibraryEntry> libraryDao)
    {
        _libraryDao = libraryDao;
    }

    public async Task<LibraryEntryModel> AddToLibrary(long comicId, ClaimsPrincipal principal)
    {
        var userId = principal.GetId();
        var entry = new LibraryEntry
        {
            ComicId = comicId,
            UserId = userId
        };
        await _libraryDao.AddAsync(entry);
        _libraryDao.SaveChanges();
        return entry.ToModel();
    }

    public async Task<IEnumerable<LibraryEntryModel>> GetLibrary(ClaimsPrincipal principal)
    {
        var userId = principal.GetId();
        var library = await _libraryDao.FindAsync(l => l.UserId == userId);
        return library.Select(l => l.ToModel());
    }

    public async Task<bool> IsInLibrary(long comicId, ClaimsPrincipal principal)
    {
        var userId = principal.GetId();
        var result = await _libraryDao.ExistsAsync(l => l.UserId == userId && l.ComicId == comicId);
        return result;
    }

    public async Task RemoveFromLibrary(long comicId, ClaimsPrincipal principal)
    {
        var userId = principal.GetId();
        var entry = (await _libraryDao.QueryAsync())
            .Where(l => l.UserId == userId && l.ComicId == comicId)
            .FirstOrDefault() ?? throw new ApiServiceException("Comic not found in library!");
        await _libraryDao.DeleteAsync(entry);
        _libraryDao.SaveChanges();
    }
}
