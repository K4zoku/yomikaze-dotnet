using Abstracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yomikaze.Application.Data.Access;
using Yomikaze.Domain.Entities;
using Yomikaze.Domain.Models;

namespace Yomikaze.Application.Data.Repos;
public class LibraryRepo(DbContext dbContext) : BaseRepo<LibraryEntry>(new LibraryDao(dbContext))
{
    public IEnumerable<LibraryEntry> GetLibraryByUserId(long userId)
    {
        return Query().Where(x => x.UserId == userId);
    }

    public LibraryEntry? GetLibraryEntry(long userId, long comicId)
    {
        return Query().FirstOrDefault(x => x.UserId == userId && x.ComicId == comicId);
    }
}
