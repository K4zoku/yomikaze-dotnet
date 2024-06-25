using Microsoft.EntityFrameworkCore;
using Yomikaze.Domain.Abstracts;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Application.Data.Access;

public class LibraryCategoryDao(DbContext dbContext) : BaseDao<LibraryCategory>(dbContext)
{
    
}