using Yomikaze.Application.Data.Access;

namespace Yomikaze.Application.Data.Repos;

public class TagCategoryRepository(DbContext dbContext) : BaseRepository<TagCategory>(new TagCategoryDao(dbContext))
{
}