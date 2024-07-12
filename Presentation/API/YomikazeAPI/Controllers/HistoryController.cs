using Microsoft.AspNetCore.JsonPatch;
using Yomikaze.Application.Helpers.API;

namespace Yomikaze.API.Main.Controllers;

[Authorize]
[Route("[controller]")]
[ApiController]
public class HistoryController(
    HistoryRepository repository,
    ComicRepository comicRepository,
    ChapterRepository chapterRepository,
    IMapper mapper,
    ILogger<HistoryController> logger) :
    SearchControllerBase<HistoryRecord, HistoryRecordModel, HistoryRepository, HistorySearchModel>(repository, mapper, logger)
{
    
    private ComicRepository ComicRepository { get; } = comicRepository;
    private ChapterRepository ChapterRepository { get; } = chapterRepository;

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
    
    [HttpGet("comics/{comicId}/continue")]
    [Authorize]
    public ActionResult<HistoryRecordModel> GetContinue(ulong comicId)
    {
        var userId = User.GetId();
        
        var comic = ComicRepository.Get(comicId);
        
        if (comic == null)
        {
            return NotFound();
        }
        
        var record = Repository.Get(userId, comic);
        if (record != null)
        {
            return new HistoryRecordModel
            {
                ChapterId = record.ChapterId.ToString(),
                PageNumber = record.PageNumber,
            };
        }

        var chapter = ChapterRepository.GetByComicIdAndIndex(comicId.ToString(), 0);
        if (chapter == null)
        {
            return NotFound();
        }
        var model = new HistoryRecordModel
        {
            ChapterId = chapter.Id.ToString(),
            PageNumber = 0,
        };
        return model;
    }

    protected override IList<SearchFieldMutator<HistoryRecord, HistorySearchModel>> SearchFieldMutators { get; } =
    [
        new SearchFieldMutator<HistoryRecord, HistorySearchModel>(search => search.OrderBy is { Length: > 0 }, (query, search) =>
        {
            IOrderedQueryable<HistoryRecord> ordered = search.OrderBy!.First() switch
            {
                HistoryOrderBy.PageNumber => query.OrderBy(x => x.PageNumber),
                HistoryOrderBy.PageNumberDesc => query.OrderByDescending(x => x.PageNumber),
                HistoryOrderBy.CreationTime => query.OrderBy(x => x.CreationTime),
                HistoryOrderBy.CreationTimeDesc => query.OrderByDescending(x => x.CreationTime),
                HistoryOrderBy.LastModified => query.OrderBy(x => x.LastModified),
                HistoryOrderBy.LastModifiedDesc => query.OrderByDescending(x => x.LastModified),
                _ => query.OrderBy(x => x.PageNumber),
            };
            return search.OrderBy!.Skip(1)
                .Aggregate(ordered, (current, orderBy) => orderBy switch
                {
                    HistoryOrderBy.PageNumber => current.ThenBy(x => x.PageNumber),
                    HistoryOrderBy.PageNumberDesc => current.ThenByDescending(x => x.PageNumber),
                    HistoryOrderBy.CreationTime => current.ThenBy(x => x.CreationTime),
                    HistoryOrderBy.CreationTimeDesc => current.ThenByDescending(x => x.CreationTime),
                    HistoryOrderBy.LastModified => current.ThenBy(x => x.LastModified),
                    HistoryOrderBy.LastModifiedDesc => current.ThenByDescending(x => x.LastModified),
                    _ => current.ThenBy(x => x.PageNumber),
                });
        }),
        new SearchFieldMutator<HistoryRecord, HistorySearchModel>(search => search.FromCreationTime.HasValue, (query, search) => query.Where(x => x.CreationTime >= search.FromCreationTime)),
        new SearchFieldMutator<HistoryRecord, HistorySearchModel>(search => search.ToCreationTime.HasValue, (query, search) => query.Where(x => x.CreationTime <= search.ToCreationTime)),
    ];
}