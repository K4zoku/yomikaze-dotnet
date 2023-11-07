using Microsoft.EntityFrameworkCore;
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

    public async Task AddToLibrary(long comicId, ClaimsPrincipal principal)
    {
        var userId = principal.GetId();
        var entry = new LibraryEntry
        {
            ComicId = comicId,
            UserId = userId
        };
        await _libraryDao.AddAsync(entry);
        _libraryDao.SaveChanges();
    }

    public async Task<IEnumerable<ComicModel>> GetLibrary(ClaimsPrincipal principal)
    {
        var userId = principal.GetId();
        var query = await _libraryDao.QueryAsync();
        var library = await query.Where(l => l.UserId == userId)
            .Include(l => l.User)
            .Include(l => l.Comic)
            .ToListAsync();
        return library.Select(l => l.Comic.ToModel());
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
            .FirstOrDefault() ?? throw new ApiServiceException("Comic is not in library");
        await _libraryDao.DeleteAsync(entry);
        _libraryDao.SaveChanges();
    }
}
