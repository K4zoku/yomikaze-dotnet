using Yomikaze.Application.Data.Repos;
using Yomikaze.Application.Helpers.API;

namespace Yomikaze.API.Main.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Administrator, Publisher")]
public class ComicsController(
    DbContext dbContext,
    IMapper mapper,
    IDistributedCache cache,
    ILogger<ComicsController> logger)
    : CrudControllerBase<Comic, ComicModel>(dbContext, mapper, new ComicRepository(dbContext), cache, logger)
{
    public override ActionResult<ComicModel> Post([Bind("Name,Description,Cover,Banner,PublicationDate,Authors,Status,TagIds")] ComicModel input)
    {
        input.PublisherId = User.GetIdString();
        return base.Post(input);
    }
}