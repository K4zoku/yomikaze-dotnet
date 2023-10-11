using Microsoft.EntityFrameworkCore;
using Yomikaze.Domain.Entities;
using Yomikaze.Infrastructure.Data;

namespace Yomikaze.Application.Data;

public class ComicDao : BaseDao<Comic, long>
{
    protected ComicDao(DbContext dbContext) : base(dbContext)
    {
        
    }
}