using Yomikaze.Application.Data.Access;

namespace Yomikaze.Application.Data.Repos;

public class ProfileReportRepository(DbContext dbContext) : BaseRepository<ProfileReport>(new ProfileReportDao(dbContext))
{
    
}