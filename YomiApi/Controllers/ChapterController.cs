using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YomiApi.Base;
using Yomikaze.Application.Data.Models.Common;
using Yomikaze.Application.Data.Repos;
using Yomikaze.Domain.Entities;

namespace YomiApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ChapterController(DbContext dbContext, IMapper mapper) : CRUDBaseController<Chapter, ChapterInputModel, ChapterOutputModel>(dbContext, mapper, new ChapterRepo(dbContext))
{
    
}
