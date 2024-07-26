namespace Yomikaze.API.Main.Controllers;

[Authorize(Roles = "Administrator")]
[Route("tags/categories")]
[ApiController]
public class TagCategoriesController(
    TagCategoryRepository repository,
    IMapper mapper,
    IDistributedCache cache,
    ILogger<CrudControllerBase<TagCategory, TagCategoryModel, TagCategoryRepository>> logger)
    : CrudControllerBase<TagCategory, TagCategoryModel, TagCategoryRepository>(repository, mapper, cache, logger)
{
    [AllowAnonymous]
    public override ActionResult<PagedList<TagCategoryModel>> List(PaginationModel pagination)
    {
        return base.List(pagination);
    }
}