using Yomikaze.Application.Data.Access;

namespace Yomikaze.Application.Data.Repos;

public class WithdrawalRequestRepository(DbContext dbContext) : BaseRepository<WithdrawalRequest>(new WithdrawalRequestDao(dbContext))
{
    
}