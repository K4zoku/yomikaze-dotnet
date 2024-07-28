using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;
using Yomikaze.Application.Helpers.API;
using Yomikaze.Infrastructure.Context;

namespace Yomikaze.API.Main.Controllers;

[ApiController]
[Route("reports/comic")]
public class ComicReportController(
    YomikazeDbContext dbContext,
    ComicReportRepository repository,
    IMapper mapper,
    ILogger<ComicReportController> logger)
    : SearchControllerBase<ComicReport, ComicReportModel, ComicReportRepository, ComicReportSearchModel>(repository,
        mapper, logger)
{
    
    private YomikazeDbContext DbContext { get; } = dbContext;
    
    protected override IList<SearchFieldMutator<ComicReport, ComicReportSearchModel>> SearchFieldMutators { get; } =
    [
        new SearchFieldMutator<ComicReport, ComicReportSearchModel>(
            search => !string.IsNullOrWhiteSpace(search.ReasonId),
            (query, search) => query.Where(x => x.ReasonId.ToString() == search.ReasonId)),
        new SearchFieldMutator<ComicReport, ComicReportSearchModel>(
            search => !string.IsNullOrWhiteSpace(search.ComicId),
            (query, search) => query.Where(x => x.ComicId.ToString() == search.ComicId)),
        new SearchFieldMutator<ComicReport, ComicReportSearchModel>(
            search => !string.IsNullOrWhiteSpace(search.ReporterId),
            (query, search) => query.Where(x => x.ReporterId.ToString() == search.ReporterId)),
        new SearchFieldMutator<ComicReport, ComicReportSearchModel>(
            search => search.Status.HasValue,
            (query, search) => query.Where(x => x.Status == search.Status!.Value)),
        new SearchFieldMutator<ComicReport, ComicReportSearchModel>(
            search => search.OrderBy is not { Length: 0 },
            (query, search) =>
            {
                var ordered = search.OrderBy[0] switch
                {
                    ComicReportOrderBy.CreationTime => query.OrderBy(x => x.CreationTime),
                    ComicReportOrderBy.CreationTimeDesc => query.OrderByDescending(x => x.CreationTime),
                    _ => query.OrderByDescending(x => x.CreationTime)
                };
                return search.OrderBy.Skip(1).Aggregate(ordered, (current, orderBy) => orderBy switch
                {
                    ComicReportOrderBy.CreationTime => current.ThenBy(x => x.CreationTime),
                    ComicReportOrderBy.CreationTimeDesc => current.ThenByDescending(x => x.CreationTime),
                    _ => current
                });
            })
    ];

    [NonAction]
    public override ActionResult<ComicReportModel> Post(ComicReportModel input)
    {
        return base.Post(input);
    }
    
    [HttpPost("{comicId}")]
    public ActionResult<ComicReportModel> Post(ulong comicId, ComicReportModel input)
    {
        input.ComicId = comicId.ToString();
        input.ReporterId = User.GetIdString();
        input.Status = ReportStatus.Pending;
        Logger.LogInformation("Creating comic report: {Input}", JsonConvert.SerializeObject(input));
        return base.Post(input);
    }
    
    [HttpGet("reasons")]
    public ActionResult<IEnumerable<ReportReasonModel>> GetReasons()
    {
        var result = DbContext.ComicReportReasons.ToList();
        return Ok(Mapper.Map<IEnumerable<ReportReasonModel>>(result));
    }
}