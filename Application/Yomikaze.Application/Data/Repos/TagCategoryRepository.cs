using Microsoft.EntityFrameworkCore;
using Yomikaze.Application.Data.Access;
using Yomikaze.Domain.Abstracts;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Application.Data.Repos;

public class TagCategoryRepository(DbContext dbContext) : BaseRepository<TagCategory>(new TagCategoryDao(dbContext))
{
    
}