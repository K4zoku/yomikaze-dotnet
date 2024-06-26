using Microsoft.EntityFrameworkCore;
using Yomikaze.Application.Data.Access;
using Yomikaze.Domain.Abstracts;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Application.Data.Repos;

public class TagRepository(DbContext dbContext) : BaseRepository<Tag>(new GenreDao(dbContext))
{
}