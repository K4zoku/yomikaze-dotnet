using Microsoft.EntityFrameworkCore;
using Yomikaze.Application.Data.Repos;
using Yomikaze.Domain.Entities;
using YomiOdata.Base;

namespace YomiOdata.Controllers;

public class ComicsOdataController(DbContext dbContext) : ODataControllerBase<Comic>(dbContext, new ComicRepo(dbContext))
{
}
