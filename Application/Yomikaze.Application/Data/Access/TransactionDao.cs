namespace Yomikaze.Application.Data.Access;

public class TransactionDao(DbContext dbContext) : BaseDao<Transaction>(dbContext)
{
}