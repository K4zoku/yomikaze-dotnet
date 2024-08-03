using Yomikaze.Application.Data.Access;

namespace Yomikaze.Application.Data.Repos;

public class ChapterCommentReportRepository(DbContext dbContext)
    : BaseRepository<ChapterCommentReport>(new ChapterCommentReportDao(dbContext))
{
}