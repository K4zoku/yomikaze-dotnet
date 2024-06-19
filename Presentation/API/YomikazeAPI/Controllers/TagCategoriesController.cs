namespace Yomikaze.API.Main.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Administrator")]
public class TagCategoriesController(DbContext dbContext, IMapper mapper, IRepository<TagCategory> repository, IDistributedCache cache, ILogger<TagCategoriesController> logger) 
    : CrudControllerBase<TagCategory, TagCategoryModel>(dbContext, mapper, repository, cache, logger)
{
}