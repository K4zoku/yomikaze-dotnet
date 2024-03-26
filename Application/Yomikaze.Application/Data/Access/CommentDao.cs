using Microsoft.EntityFrameworkCore;
using Yomikaze.Domain.Abstracts;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Application.Data.Access;

public class CommentDao(DbContext dbContext) : BaseDao<Comment>(dbContext)
{
}