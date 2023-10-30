using Yomikaze.Domain.Common;
using Yomikaze.Domain.Database.Entities;
using Yomikaze.Infrastructure.Data;

namespace Yomikaze.Application.Data.Access;

public class CommentDao : BaseDao<Comment>, IDao<Comment>
{
    public CommentDao(YomikazeDbContext dbContext) : base(dbContext) { }
}
