using Yomikaze.Application.Data.Access;

namespace Yomikaze.Application.Data.Repos;

public class ComicReportRepository(DbContext dbContext) : BaseRepository<ComicReport>(new ComicReportDao(dbContext))
{
}