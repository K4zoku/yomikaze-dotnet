using Yomikaze.Application.Data.Access;

namespace Yomikaze.Application.Data.Repos;

public class ChapterReportRepository(DbContext dbContext)
    : BaseRepository<ChapterReport>(new ChapterReportDao(dbContext))
{
}