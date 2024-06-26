namespace Yomikaze.API.Main.Controllers;

[Authorize(Roles = "Administrator,Publisher,Reader")]
[Route("tags/categories")]
[ApiController]
public class TagCategoriesController(
    TagCategoryRepository repository,
    IMapper mapper,
    IDistributedCache cache,
    ILogger<CrudControllerBase<TagCategory, TagCategoryModel, TagCategoryRepository>> logger)
    : CrudControllerBase<TagCategory, TagCategoryModel, TagCategoryRepository>(repository, mapper, cache, logger)
{
}