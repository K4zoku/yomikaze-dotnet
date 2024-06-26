namespace Yomikaze.API.Main.Controllers;

[Authorize(Roles = "Administrator,Publisher,Reader")]
[Route("[controller]")]
[ApiController]
public class LibraryController(LibraryRepository repository, IMapper mapper, IDistributedCache cache, ILogger<LibraryController> logger) 
    : CrudControllerBase<LibraryEntry, LibraryEntryModel, LibraryRepository>(repository, mapper, cache, logger)
{
    
}