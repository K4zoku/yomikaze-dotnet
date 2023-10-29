using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Yomikaze.Domain.Common;
using Yomikaze.Domain.Database.Entities;
using Yomikaze.Infrastructure.Data;

namespace Yomikaze.Application.Data.Access
{
    public class CommentDao : BaseDao<Comment>, IDao<Comment>
    {
        public CommentDao(YomikazeDbContext dbContext) : base(dbContext) { }
    }
}
