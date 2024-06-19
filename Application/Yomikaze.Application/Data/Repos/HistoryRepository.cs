using Microsoft.EntityFrameworkCore;
using Yomikaze.Application.Data.Access;
using Yomikaze.Domain.Abstracts;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Application.Data.Repos;

public class HistoryRepository(DbContext dbContext) : BaseRepository<HistoryRecord>(new HistoryDao(dbContext))
{
    public IQueryable<HistoryRecord> GetHistoryByUserId(string userId)
    {
        return Query()
            .Where(x => x.UserId.ToString() == userId);
    }
}