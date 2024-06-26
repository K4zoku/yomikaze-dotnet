namespace Yomikaze.API.Main.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Administrator")]
public class TagsController(
    TagRepository dbContext,
    IMapper mapper,
    IDistributedCache cache,
    ILogger<TagsController> logger)
    : CrudControllerBase<Tag, TagModel, TagRepository>(dbContext, mapper, cache, logger)
{
}