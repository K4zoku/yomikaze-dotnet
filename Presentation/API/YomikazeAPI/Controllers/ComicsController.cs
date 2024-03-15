using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yomikaze.API.Main.Base;
using Yomikaze.Application.Data.Repos;
using Yomikaze.Domain.Entities;
using Yomikaze.Domain.Models;

namespace Yomikaze.API.Main.Controllers;

[ApiController]
[Route("[controller]")]
public class ComicsController(DbContext dbContext, IMapper mapper)
    : CrudControllerBase<Comic, ComicInputModel, ComicOutputModel>(dbContext, mapper, new ComicRepo(dbContext))
{
}