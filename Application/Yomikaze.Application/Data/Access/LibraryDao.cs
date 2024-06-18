using Microsoft.EntityFrameworkCore;
using Yomikaze.Domain.Abstracts;
using Yomikaze.Domain.Entities;
using Yomikaze.Domain.Entities.Weak;

namespace Yomikaze.Application.Data.Access;

public class LibraryDao(DbContext dbContext) : BaseDao<LibraryEntry>(dbContext)
{
}