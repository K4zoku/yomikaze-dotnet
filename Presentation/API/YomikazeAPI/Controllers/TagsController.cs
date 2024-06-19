using Yomikaze.Application.Data.Repos;

namespace Yomikaze.API.Main.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Administrator")]
public class TagsController(
    DbContext dbContext,
    IMapper mapper,
    IDistributedCache cache,
    ILogger<TagsController> logger)
    : CrudControllerBase<Tag, TagModel>(dbContext, mapper, new GenreRepository(dbContext), cache, logger)
{
}