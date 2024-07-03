using Microsoft.AspNetCore.JsonPatch;
using Yomikaze.Application.Helpers.API;

namespace Yomikaze.API.Main.Controllers;

[Authorize(Roles = "Administrator,Publisher,Reader")]
[Route("[controller]")]
[ApiController]
public class HistoryController(
    HistoryRepository repository,
    IMapper mapper,
    ILogger<HistoryController> logger) :
    CrudControllerBase<HistoryRecord, HistoryRecordModel, HistoryRepository>(repository, mapper, logger)
{
    [HttpGet]
    [Authorize]
    public override ActionResult<PagedList<HistoryRecordModel>> List([FromQuery] PaginationModel pagination)
    {
        return base.List(pagination);
    }

    protected override IQueryable<HistoryRecord> GetQuery()
    {
        return Repository.GetAllByUserId(User.GetIdString());
    }

    [NonAction]
    public override ActionResult<HistoryRecordModel> Post(HistoryRecordModel input)
    {
        throw new NotSupportedException();
    }

    [NonAction]
    public override ActionResult<HistoryRecordModel> Put(ulong key, HistoryRecordModel input)
    {
        throw new NotSupportedException();
    }

    [NonAction]
    public override ActionResult<HistoryRecordModel> Patch(ulong key, JsonPatchDocument<HistoryRecordModel> patch)
    {
        throw new NotSupportedException();
    }
    
    [HttpPost("comics/{comicId}/chapters/{number}")] // mark as read
    [Authorize]
    public IActionResult Post([FromRoute] ulong comicId, [FromRoute] int number)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (Repository.Get(User.GetId(), comicId, number) != null)
        {
            return Conflict();
        }
        
        Repository.AddBy(User.GetId(), comicId, number);
        
        return NoContent();
    }
    
    [HttpDelete("comics/{comicId}/chapters/{number}")] // mark as unread
    [Authorize]
    public IActionResult Delete([FromRoute] ulong comicId, [FromRoute] int number)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (!Repository.Delete(User.GetId(), comicId, number))
        {
            return NotFound();
        }
        
        return NoContent();
    }

    [NonAction]
    public override ActionResult Delete(ulong key)
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