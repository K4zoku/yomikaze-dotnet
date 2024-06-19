using Microsoft.EntityFrameworkCore;
using Yomikaze.API.OData.Base;
using Yomikaze.Application.Data.Repos;
using Yomikaze.Domain.Entities;

namespace Yomikaze.API.OData.Controllers;

public class ChaptersController(DbContext dbContext)
    : ODataControllerBase<Chapter>(new ChapterRepository(dbContext))
{
}