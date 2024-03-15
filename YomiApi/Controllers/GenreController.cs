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
public class GenreController(DbContext dbContext, IMapper mapper) : CRUDBaseController<Genre, GenreInputModel, GenreOutputModel>(dbContext, mapper, new GenreRepo(dbContext))
{
}
