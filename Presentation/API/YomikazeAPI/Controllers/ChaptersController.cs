using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yomikaze.API.Main.Base;
using Yomikaze.Application.Data.Repos;
using Yomikaze.Domain.Entities;
using Yomikaze.Domain.Models;

namespace Yomikaze.API.Main.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Administrator")]
public class ChaptersController(DbContext dbContext, IMapper mapper)
    : CrudControllerBase<Chapter, ChapterInputModel, ChapterOutputModel>(dbContext, mapper, new ChapterRepo(dbContext))
{
}