using Microsoft.AspNetCore.JsonPatch;
using Yomikaze.Application.Data.Repos;
using Yomikaze.Application.Helpers.API;

namespace Yomikaze.API.Main.Controllers;

[Authorize(Roles = "Administrator,Publisher,Reader")]
[Route("[controller]")]
[ApiController]
public class HistoryController(DbContext dbContext, IMapper mapper, IDistributedCache cache, ILogger<CrudControllerBase<HistoryRecord, HistoryRecordModel>> logger) : 
    CrudControllerBase<HistoryRecord, HistoryRecordModel>(dbContext, mapper, new HistoryRepository(dbContext), cache, logger)
{
    
    private HistoryRepository HistoryRepository => (HistoryRepository)Repository;
    
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
    
    [NonAction]
    public override ActionResult<HistoryRecordModel> Patch(ulong key, JsonPatchDocument<HistoryRecordModel> patchDocument)
    {
        return NotFound();
    }
    
    [HttpDelete]
    public IActionResult Clear()
    {
        HistoryRepository.Clear(User.GetIdString());
        return NoContent();
    }
}