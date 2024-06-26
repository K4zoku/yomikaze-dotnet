namespace Yomikaze.API.Main.Controllers;

public class LibraryCategoriesController(LibraryCategoryRepository repository, IMapper mapper, IDistributedCache cache, ILogger<CrudControllerBase<LibraryCategory, LibraryCategoryModel, LibraryCategoryRepository>> logger) 
    : CrudControllerBase<LibraryCategory, LibraryCategoryModel, LibraryCategoryRepository>(repository, mapper, cache, logger)
{
    
}