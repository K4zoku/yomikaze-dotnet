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
public class ComicController(DbContext dbContext, IMapper mapper) : CRUDBaseController<Comic, ComicInputModel, ComicOutputModel>(dbContext, mapper, new ComicRepo(dbContext))
{

}
