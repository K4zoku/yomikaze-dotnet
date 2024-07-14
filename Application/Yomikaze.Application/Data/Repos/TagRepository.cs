using Yomikaze.Application.Data.Access;

namespace Yomikaze.Application.Data.Repos;

public class TagRepository(DbContext dbContext) : BaseRepository<Tag>(new GenreDao(dbContext))
{
}