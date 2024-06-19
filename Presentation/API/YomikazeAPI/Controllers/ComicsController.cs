using Yomikaze.Application.Data.Repos;

namespace Yomikaze.API.Main.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Administrator")]
public class ComicsController(
    DbContext dbContext,
    IMapper mapper,
    IDistributedCache cache,
    ILogger<ComicsController> logger)
    : CrudControllerBase<Comic, ComicModel>(dbContext, mapper, new ComicRepository(dbContext), cache, logger)
{
}