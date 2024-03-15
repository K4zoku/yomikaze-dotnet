using Microsoft.EntityFrameworkCore;
using Yomikaze.Application.Data.Repos;
using Yomikaze.Domain.Entities;
using YomiOdata.Base;

namespace YomiOdata.Controllers;

public class CommentsOdataController(DbContext dbContext) : ODataControllerBase<Comment>(dbContext, new CommentRepo(dbContext))
{
}
