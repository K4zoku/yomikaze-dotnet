using Microsoft.EntityFrameworkCore;
using Yomikaze.Domain.Abstracts;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Application.Data.Access;

public class LibraryDao(DbContext dbContext) : BaseDao<LibraryEntry>(dbContext)
{
    public override IQueryable<LibraryEntry> Query()
    {
        return base.Query()
            .Include(library => library.Comic);
    }
}