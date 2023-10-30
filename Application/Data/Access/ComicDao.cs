using Yomikaze.Domain.Common;
using Yomikaze.Domain.Database.Entities;
using Yomikaze.Infrastructure.Data;

namespace Yomikaze.Application.Data.Access;

public class ComicDao : BaseDao<Comic>, IDao<Comic>
{
    public ComicDao(YomikazeDbContext dbContext) : base(dbContext) { }
}