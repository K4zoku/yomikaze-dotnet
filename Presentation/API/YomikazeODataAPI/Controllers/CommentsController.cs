using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Yomikaze.API.OData.Base;
using Yomikaze.Application.Data.Repos;
using Yomikaze.Domain.Entities;

namespace Yomikaze.API.OData.Controllers;

[Authorize]
public class CommentsController(DbContext dbContext)
    : ODataControllerBase<Comment>(new CommentRepository(dbContext))
{
}