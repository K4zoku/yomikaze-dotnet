namespace Yomikaze.API.Main.Controllers;

[Authorize(Roles = "Administrator,Publisher,Reader")]
[Route("library/categories")]
[ApiController]
public class LibraryCategoriesController(
    LibraryCategoryRepository repository,
    IMapper mapper,
    ILogger<CrudControllerBase<LibraryCategory, LibraryCategoryModel, LibraryCategoryRepository>> logger)
    : CrudControllerBase<LibraryCategory, LibraryCategoryModel, LibraryCategoryRepository>(repository, mapper, logger)
{
}