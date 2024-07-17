namespace Yomikaze.API.Main.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Super,Administrator")]
public class TagsController(
    TagRepository dbContext,
    IMapper mapper,
    IDistributedCache cache,
    ILogger<TagsController> logger)
    : CrudControllerBase<Tag, TagModel, TagRepository>(dbContext, mapper, cache, logger)
{
    [AllowAnonymous]
    public override ActionResult<PagedList<TagModel>> List(PaginationModel pagination)
    {
        return base.List(pagination);
    }
}