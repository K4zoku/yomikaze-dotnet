using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yomikaze.API.Main.Base;
using Yomikaze.Application.Data.Repos;
using Yomikaze.Domain.Entities;
using Yomikaze.Domain.Models.Common;

namespace Yomikaze.API.Main.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentsController(DbContext dbContext, IMapper mapper)
    : CrudControllerBase<Comment, CommentInputModel, CommentOutputModel>(dbContext, mapper, new CommentRepo(dbContext))
{
}