using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;
using Yomikaze.Application.Helpers.API;
using Yomikaze.Infrastructure.Context;

namespace Yomikaze.API.Main.Controllers;

[ApiController]
[Route("/reports/chapters")]
public class ChapterReportController(
    YomikazeDbContext dbContext,
    ChapterReportRepository repository,
    IMapper mapper,
    ILogger<ChapterReportController> logger)
    : SearchControllerBase<ChapterReport, ChapterReportModel, ChapterReportRepository, ChapterReportSearchModel>(repository,
        mapper, logger)
{
    
    private YomikazeDbContext DbContext { get; } = dbContext;
    
    protected override IList<SearchFieldMutator<ChapterReport, ChapterReportSearchModel>> SearchFieldMutators { get; } =
    [
        new SearchFieldMutator<ChapterReport, ChapterReportSearchModel>(
            search => !string.IsNullOrWhiteSpace(search.ReasonId),
            (query, search) => query.Where(x => x.ReasonId.ToString() == search.ReasonId)),
        new SearchFieldMutator<ChapterReport, ChapterReportSearchModel>(
            search => !string.IsNullOrWhiteSpace(search.ComicId),
            (query, search) => query.Where(x => x.Chapter.ComicId.ToString() == search.ComicId)),
        new SearchFieldMutator<ChapterReport, ChapterReportSearchModel>(
            search => search.ChapterNumber.HasValue,
            (query, search) => query.Where(x => x.Chapter.Number == search.ChapterNumber!.Value)),
        new SearchFieldMutator<ChapterReport, ChapterReportSearchModel>(
            search => !string.IsNullOrWhiteSpace(search.ReporterId),
            (query, search) => query.Where(x => x.ReporterId.ToString() == search.ReporterId)),
        new SearchFieldMutator<ChapterReport, ChapterReportSearchModel>(
            search => search.Status.HasValue,
            (query, search) => query.Where(x => x.Status == search.Status!.Value)),
        new SearchFieldMutator<ChapterReport, ChapterReportSearchModel>(
            search => search.OrderBy is not { Length: 0 },
            (query, search) =>
            {
                var ordered = search.OrderBy[0] switch
                {
                    ChapterReportOrderBy.CreationTime => query.OrderBy(x => x.CreationTime),
                    ChapterReportOrderBy.CreationTimeDesc => query.OrderByDescending(x => x.CreationTime),
                    _ => query.OrderByDescending(x => x.CreationTime)
                };
                return search.OrderBy.Skip(1).Aggregate(ordered, (current, orderBy) => orderBy switch
                {
                    ChapterReportOrderBy.CreationTime => current.ThenBy(x => x.CreationTime),
                    ChapterReportOrderBy.CreationTimeDesc => current.ThenByDescending(x => x.CreationTime),
                    _ => current
                });
            })
    ];

    [NonAction]
    public override ActionResult<ChapterReportModel> Post(ChapterReportModel input)
    {
        return base.Post(input);
    }
    
    [HttpPost("comics/{comicId}/chapters/{chapterNumber:int}")]
    public ActionResult<ChapterReportModel> Post(ulong comicId, int chapterNumber, ChapterReportModel input, [FromServices] ChapterRepository chapterRepository)
    {
        var chapter = chapterRepository.GetByComicIdAndIndex(comicId.ToString(), chapterNumber);
        if (chapter == null)
        {
            return NotFound();
        }
        input.ChapterId = chapter.Id.ToString();
        input.ReporterId = User.GetIdString();
        input.Status = ReportStatus.Pending;
        Logger.LogInformation("Creating comic report: {Input}", JsonConvert.SerializeObject(input));
        return base.Post(input);
    }
    
    [HttpGet("reasons")]
    public ActionResult<IEnumerable<ReportReasonModel>> GetReasons()
    {
        var result = DbContext.ChapterReportReasons.ToList();
        return Ok(Mapper.Map<IEnumerable<ReportReasonModel>>(result));
    }
}