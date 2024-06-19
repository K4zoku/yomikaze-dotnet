using Microsoft.EntityFrameworkCore;
using Yomikaze.Domain.Abstracts;
using Yomikaze.Domain.Entities;
using Yomikaze.Domain.Entities.Weak;

namespace Yomikaze.Application.Data.Access;

public class HistoryDao(DbContext dbContext) : BaseDao<HistoryRecord>(dbContext)
{
}