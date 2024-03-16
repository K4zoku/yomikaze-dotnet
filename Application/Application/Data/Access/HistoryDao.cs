using Abstracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Application.Data.Access;
public class HistoryDao(DbContext dbContext) : BaseDao<HistoryRecord>(dbContext)
{
}
