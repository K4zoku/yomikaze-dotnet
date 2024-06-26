namespace Yomikaze.API.Main.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Administrator,Publisher")]
public class ChaptersController(ChapterRepository repository, IMapper mapper, IDistributedCache cache, ILogger<ChaptersController> logger) 
    : CrudControllerBase<Chapter, ChapterModel, ChapterRepository>(repository, mapper, cache, logger)
{
}