using Yomikaze.Application.Data.Access;

namespace Yomikaze.Application.Data.Repos;

public class CommentReportRepository(DbContext dbContext)
    : BaseRepository<CommentReport>(new CommentReportDao(dbContext))
{
}