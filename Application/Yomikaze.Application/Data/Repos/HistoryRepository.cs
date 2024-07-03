using Microsoft.EntityFrameworkCore;
using Yomikaze.Application.Data.Access;
using Yomikaze.Domain.Abstracts;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Application.Data.Repos;

public class HistoryRepository(DbContext dbContext) : BaseRepository<HistoryRecord>(new HistoryDao(dbContext))
{
    public IQueryable<HistoryRecord> GetAllByUserId(string userId)
    {
        return Query()
            .Where(x => x.UserId.ToString() == userId);
    }
    
    public HistoryRecord? Get(ulong userId, ulong comicId, int chapterNumber)
    {
        return Query()
            .FirstOrDefault(x => x.UserId == userId && x.Chapter.ComicId == comicId && x.Chapter.Number == chapterNumber);
    }

    public void Clear(string userId)
    {
        Dao.DeleteAll(x => x.UserId.ToString() == userId);
    }
}