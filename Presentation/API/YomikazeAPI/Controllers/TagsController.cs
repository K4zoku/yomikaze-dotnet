using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Net;
using Yomikaze.API.Main.Base;
using Yomikaze.Application.Data.Repos;
using Yomikaze.Application.Helpers;
using Yomikaze.Domain.Entities;
using Yomikaze.Domain.Models;

namespace Yomikaze.API.Main.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Administrator")]
public class TagsController(DbContext dbContext, IMapper mapper, IDistributedCache cache, ILogger<TagsController> logger)
    : CrudControllerBase<Tag, TagModel>(dbContext, mapper, new GenreRepository(dbContext), cache, logger)
{
}