using Abstracts;
using Microsoft.EntityFrameworkCore;
using Yomikaze.Application.Data.Access;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Application.Data.Repos;

public class GenreRepo(DbContext dbContext) : BaseRepo<Genre>(new GenreDao(dbContext))
{
}