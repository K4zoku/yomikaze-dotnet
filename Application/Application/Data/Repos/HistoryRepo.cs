using Abstracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yomikaze.Application.Data.Access;
using Yomikaze.Domain.Entities;
using Yomikaze.Domain.Models;

namespace Yomikaze.Application.Data.Repos;
public class HistoryRepo(DbContext dbContext) : BaseRepo<HistoryRecord>(new HistoryDao(dbContext))
{
    public IEnumerable<HistoryRecord> GetHistoryByUserId(long userId)
    {
        return Query()
            .Include(x => x.Chapter)
            .ThenInclude(x => x.Comic)
            .Where(x => x.UserId == userId);
    }
}
