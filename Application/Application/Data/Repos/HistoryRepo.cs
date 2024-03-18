using Microsoft.EntityFrameworkCore;
using Yomikaze.Application.Data.Access;
using Yomikaze.Domain.Abstracts;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Application.Data.Repos;

public class HistoryRepo(DbContext dbContext) : BaseRepo<HistoryRecord>(new HistoryDao(dbContext))
{
    public IEnumerable<HistoryRecord> GetHistoryByUserId(ulong userId)
    {
        return Query()
            .Include(x => x.Chapter)
            .ThenInclude(x => x.Comic)
            .Where(x => x.UserId == userId);
    }
}