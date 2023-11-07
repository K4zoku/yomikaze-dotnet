using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Yomikaze.Application.Data.Models;
using Yomikaze.Application.Data.Models.Common;
using Yomikaze.Domain.Common;
using Yomikaze.Domain.Database.Entities;
using Yomikaze.Domain.Database.Entities.Identity;

namespace Yomikaze.WebAPI.Services;

public class LibraryService
{
    private readonly IDao<LibraryEntry> _libraryDao;
    private readonly IDao<Comic> _comicDao;
    private readonly UserManager<User> _userManager;

    public LibraryService(IDao<LibraryEntry> libraryDao, IDao<Comic> comicDao, UserManager<User> userManager)
    {
        _libraryDao = libraryDao;
        _comicDao = comicDao;
        _userManager = userManager;
    }

    public async Task AddToLibrary(long comicId, ClaimsPrincipal principal)
    {
        var user = await _userManager.GetUserAsync(principal) ?? throw new ApiServiceException("User not found");
        var comic = await _comicDao.GetAsync(comicId) ?? throw new ApiServiceException("Comic not found");
        var entry = new LibraryEntry
        {
            ComicId = comic.Id,
            UserId = user.Id
        };
        if (await _libraryDao.ExistsAsync(l => l.UserId == user.Id && l.ComicId == comic.Id))
        {
            throw new ApiServiceException("Comic is already in library");
        }
        await _libraryDao.AddAsync(entry);
        _libraryDao.SaveChanges();
    }

    public async Task<IEnumerable<ComicModel>> GetLibrary(ClaimsPrincipal principal)
    {
        var user = await _userManager.GetUserAsync(principal) ?? throw new ApiServiceException("User not found");
        var query = await _libraryDao.QueryAsync();
        var library = await query.Where(l => l.UserId == user.Id)
            .Include(l => l.User)
            .Include(l => l.Comic)
            .ToListAsync();
        return library.Select(l => l.Comic.ToModel());
    }

    public async Task<bool> IsInLibrary(long comicId, ClaimsPrincipal principal)
    {
        var user = await _userManager.GetUserAsync(principal) ?? throw new ApiServiceException("User not found");
        var comic = await _comicDao.GetAsync(comicId) ?? throw new ApiServiceException("Comic not found");
        var result = await _libraryDao.ExistsAsync(l => l.UserId == user.Id && l.ComicId == comic.Id);
        return result;
    }

    public async Task RemoveFromLibrary(long comicId, ClaimsPrincipal principal)
    {
        var user = await _userManager.GetUserAsync(principal) ?? throw new ApiServiceException("User not found");
        var comic = await _comicDao.GetAsync(comicId) ?? throw new ApiServiceException("Comic not found");
        var entry = (await _libraryDao.QueryAsync())
            .Where(l => l.UserId == user.Id && l.ComicId == comic.Id)
            .FirstOrDefault() ?? throw new ApiServiceException("Comic is not in library");
        await _libraryDao.DeleteAsync(entry);
        _libraryDao.SaveChanges();
    }
}
