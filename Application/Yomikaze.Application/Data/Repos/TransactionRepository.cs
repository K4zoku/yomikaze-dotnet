using Yomikaze.Application.Data.Access;

namespace Yomikaze.Application.Data.Repos;

public class TransactionRepository(DbContext dbContext) : BaseRepository<Transaction>(new TransactionDao(dbContext))
{
    public IQueryable<Transaction> GetAllByUserId(string userId)
    {
        return Query()
            .Where(transaction => transaction.UserId.ToString() == userId);
    }
}