using Microsoft.AspNetCore.JsonPatch;
using Yomikaze.Application.Helpers.API;

namespace Yomikaze.API.Main.Controllers;

[Authorize]
[Route("[controller]")]
[ApiController]
public class HistoryController(
    HistoryRepository repository,
    IMapper mapper,
    ILogger<HistoryController> logger) :
    CrudControllerBase<HistoryRecord, HistoryRecordModel, HistoryRepository>(repository, mapper, logger)
{

    protected override IQueryable<HistoryRecord> ListQuery()
    {
        return Repository.GetAllByUserId(User.GetIdString());
    }

    [NonAction]
    public override ActionResult<HistoryRecordModel> Post(HistoryRecordModel input)
    {
        throw new NotSupportedException();
    }

    [NonAction]
    public override ActionResult<HistoryRecordModel> Patch(ulong key, JsonPatchDocument<HistoryRecordModel> patch)
    {
        throw new NotSupportedException();
    }

    [HttpPatch("comics/{comicId}/chapters/{number:int}")]
    [Authorize]
    public IActionResult Patch([FromRoute] ulong comicId, [FromRoute] int number, JsonPatchDocument<HistoryRecordModel> patchDocument)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var record = Repository.Get(User.GetId(), comicId, number);
        if (record == null)
        {
            return NotFound();
        }

        var model = Mapper.Map<HistoryRecordModel>(record);
        patchDocument.ApplyTo(model, ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Mapper.Map(model, record);
        Repository.Update(record);
        return NoContent();
    }

    [HttpDelete]
    public IActionResult Clear()
    {
        Repository.Clear(User.GetIdString());
        return NoContent();
    }
}