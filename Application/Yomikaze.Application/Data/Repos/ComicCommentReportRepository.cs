using Yomikaze.Application.Data.Access;

namespace Yomikaze.Application.Data.Repos;

public class ComicCommentReportRepository(DbContext dbContext) : BaseRepository<ComicCommentReport>(new ComicCommentReportDao(dbContext))
{
    
}