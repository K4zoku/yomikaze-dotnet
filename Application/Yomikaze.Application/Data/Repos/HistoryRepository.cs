using Yomikaze.Application.Data.Access;

namespace Yomikaze.Application.Data.Repos;

public class HistoryRepository(DbContext dbContext) : BaseRepository<HistoryRecord>(new HistoryDao(dbContext))
{
    private ChapterRepository ChapterRepository { get; } = new(dbContext);

    public IQueryable<HistoryRecord> GetAllByUserId(string userId)
    {
        return Query()
            .Where(x => x.UserId.ToString() == userId)
            .Include(x => x.Chapter)
            .ThenInclude(x => x.Comic)
            .ThenInclude(x => x.ComicTags);
    }

    public HistoryRecord? Get(ulong userId, ulong comicId, int chapterNumber)
    {
        return Query()
            .Include(x => x.Chapter)
            .FirstOrDefault(
                x => x.UserId == userId && x.Chapter.ComicId == comicId && x.Chapter.Number == chapterNumber);
    }

    public HistoryRecord? Get(ulong userId, Chapter chapter)
    {
        return Get(userId, chapter.Id);
    }

    public HistoryRecord? Get(ulong userId, ulong chapterId)
    {
        return Query()
            .Include(x => x.Chapter)
            .FirstOrDefault(x => x.UserId == userId && x.Chapter.Id == chapterId);
    }

    public HistoryRecord? Get(ulong userId, Comic comic)
    {
        return Query()
            .Include(x => x.Chapter)
            .OrderByDescending(x => x.Chapter.Number)
            .ThenByDescending(x => x.LastModified)
            .ThenByDescending(x => x.CreationTime)
            .FirstOrDefault(x => x.UserId == userId && x.Chapter.ComicId == comic.Id);
    }

    public bool Exists(ulong userId, Chapter chapter)
    {
        return Exists(userId, chapter.Id);
    }

    public bool Exists(ulong userId, ulong chapterId)
    {
        return Query().Any(x => x.UserId == userId && x.Chapter.Id == chapterId);
    }

    public bool Exists(ulong userId, Comic comic)
    {
        return Query().Any(x => x.UserId == userId && x.Chapter.ComicId == comic.Id);
    }

    public bool Delete(ulong userId, ulong comicId, int chapterNumber)
    {
        HistoryRecord? record = Get(userId, comicId, chapterNumber);
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

    public void Add(ulong userId, Chapter chapter)
    {
        if (Exists(userId, chapter))
        {
            return;
        }

        HistoryRecord record = new HistoryRecord { UserId = userId, ChapterId = chapter.Id };

        Add(record);
    }
}