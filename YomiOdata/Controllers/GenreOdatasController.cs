using Microsoft.EntityFrameworkCore;
using Yomikaze.Application.Data.Repos;
using Yomikaze.Domain.Entities;
using YomiOdata.Base;

namespace YomiOdata.Controllers;

public class GenreOdatasController(DbContext dbContext) : ODataControllerBase<Genre>(dbContext, new GenreRepo(dbContext))
{
}
