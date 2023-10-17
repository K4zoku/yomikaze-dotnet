using Microsoft.EntityFrameworkCore;
using Yomikaze.Domain.Common;
using Yomikaze.Domain.Database.Entities;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Application.Data.Access;

public class ComicDao : BaseDao<Comic>, IDao<Comic>
{
    protected ComicDao(DbContext dbContext) : base(dbContext) { }
}