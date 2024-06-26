﻿using Microsoft.EntityFrameworkCore;
using Yomikaze.Application.Data.Access;
using Yomikaze.Domain.Abstracts;
using Yomikaze.Domain.Entities;
using Yomikaze.Domain.Entities.Weak;

namespace Yomikaze.Application.Data.Repos;

public class LibraryRepository(DbContext dbContext) : BaseRepository<LibraryEntry>(new LibraryDao(dbContext))
{
    public IEnumerable<LibraryEntry> GetLibraryByUserId(string userId)
    {
        return Query().Where(x => x.UserId.ToString() == userId);
    }

    public LibraryEntry? GetLibraryEntry(ulong userId, ulong comicId)
    {
        return Query().FirstOrDefault(x => x.UserId == userId && x.ComicId == comicId);
    }
}