using Yomikaze.Application.Data.Access;

namespace Yomikaze.Application.Data.Repos;

public class CoinPricingRepository(DbContext dbContext) : BaseRepository<CoinPricing>(new CoinPricingDao(dbContext))
{
    
}