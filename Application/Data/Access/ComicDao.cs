using Microsoft.EntityFrameworkCore;
using Yomikaze.Domain.Common;
using Yomikaze.Domain.Database.Entities;
using Yomikaze.Infrastructure.Data;

namespace Yomikaze.Application.Data.Access;

public class ComicDao : BaseDao<Comic>, IDao<Comic>
{
    public ComicDao(YomikazeDbContext dbContext) : base(dbContext) { }

    public override async Task<Comic?> GetAsync(long id)
    {
        return await DbSet
            .Where(entity => entity.Id.Equals(id))
            .Include(entity => entity.Genres)
            .Include(entity => entity.Chapters)
            .ThenInclude(entity => entity.Pages)
            .FirstOrDefaultAsync();
    }
}