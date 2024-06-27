using Microsoft.EntityFrameworkCore;
using Yomikaze.Application.Data.Access;
using Yomikaze.Domain.Abstracts;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Application.Data.Repos;

public class LibraryCategoryRepository(DbContext dbContext)
    : BaseRepository<LibraryCategory>(new LibraryCategoryDao(dbContext))
{
    public LibraryCategory? GetByNameAndUserId(string name, ulong userId)
    {
        return Query().FirstOrDefault(x => x.Name == name && x.UserId == userId);
    }
}