using Microsoft.EntityFrameworkCore;
using Yomikaze.API.OData.Base;
using Yomikaze.Application.Data.Repos;
using Yomikaze.Domain.Entities;

namespace Yomikaze.API.OData.Controllers;

public class CommentsController(DbContext dbContext)
    : ODataControllerBase<Comment>(new CommentRepo(dbContext))
{
}