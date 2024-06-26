using Yomikaze.Application.Helpers.API;

namespace Yomikaze.API.Main.Controllers;

[Authorize(Roles = "Administrator,Publisher,Reader")]
[Route("[controller]")]
[ApiController]
public class HistoryController(HistoryRepository repository, IMapper mapper, IDistributedCache cache, ILogger<HistoryController> logger) : 
    CrudControllerBase<HistoryRecord, HistoryRecordModel, HistoryRepository>(repository, mapper, cache, logger)
{
    
    [NonAction]
    public override ActionResult<HistoryRecordModel> Post(HistoryRecordModel input)
    {
        return NotFound();
    }

    [NonAction]
    public override ActionResult<HistoryRecordModel> Put(ulong key, HistoryRecordModel input)
    {
        return NotFound();
    }
    
    [HttpDelete]
    public IActionResult Clear()
    {
        Repository.Clear(User.GetIdString());
        return NoContent();
    }
}