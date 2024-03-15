using Microsoft.EntityFrameworkCore;
using Yomikaze.Application.Data.Access;
using Yomikaze.Domain.Common;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Application.Data.Repos;

public class ChapterRepo(DbContext dbContext) : BaseRepo<Chapter>(new ChapterDao(dbContext))
{
}