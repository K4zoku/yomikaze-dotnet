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
    
    public bool Delete(ulong userId, ulong comicId, int chapterNumber)
    {
        var record = Get(userId, comicId, chapterNumber);
        if (record == null)
        {
            return false;
        }
        
        Delete(record);
        return true;
    }

    public void Clear(string userId)
    {
        Dao.DeleteAll(x => x.UserId.ToString() == userId);
    }

    public void AddBy(ulong userId, ulong comicId, int chapterNumber)
    {
        var record = new HistoryRecord
        {
            UserId = userId,
            Chapter = new Chapter
            {
                ComicId = comicId,
                Number = chapterNumber
            }
        };
        
        Add(record);
    }
}