using Microsoft.EntityFrameworkCore;
using Yomikaze.Domain.Common;
using Yomikaze.Domain.Database.Entities;
using Yomikaze.Infrastructure.Data;

namespace Yomikaze.Application.Data.Access;
public class LibraryDao : BaseDao<LibraryEntry>, IDao<LibraryEntry>
{

    public LibraryDao(YomikazeDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<IEnumerable<LibraryEntry>> GetAllAsync()
    {
        return await DbSet.Include(l => l.Comic).Include(l => l.User).ToListAsync();
    }

    public override async Task<LibraryEntry?> GetAsync(long id)
    {
        return await DbSet.Include(l => l.Comic).Include(l => l.User).FirstOrDefaultAsync(g => g.Id == id);
    }

}
