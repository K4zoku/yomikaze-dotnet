using Microsoft.EntityFrameworkCore;
using Yomikaze.Domain.Common;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Application.Data;

public class ComicDao : BaseDao<Comic>
{
    protected ComicDao(DbContext dbContext) : base(dbContext) { }
}