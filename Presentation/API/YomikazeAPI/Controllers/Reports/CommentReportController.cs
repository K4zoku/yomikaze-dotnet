using Newtonsoft.Json;
using Yomikaze.Application.Helpers.API;
using Yomikaze.Domain.Models.Search;
using Yomikaze.Infrastructure.Context;

namespace Yomikaze.API.Main.Controllers.Reports;

[ApiController]
[Route("/reports/comment")]
public class CommentReportController(
    YomikazeDbContext dbContext,
    CommentReportRepository repository,
    IMapper mapper,
    ILogger<CommentReportController> logger)
    : SearchControllerBase<CommentReport, CommentReportModel, CommentReportRepository, CommentReportSearchModel>(
        repository,
        mapper, logger)
{
    private YomikazeDbContext DbContext { get; } = dbContext;

    protected override IList<SearchFieldMutator<CommentReport, CommentReportSearchModel>> SearchFieldMutators { get; } =
    [
        new SearchFieldMutator<CommentReport, CommentReportSearchModel>(
            search => !string.IsNullOrWhiteSpace(search.ReasonId),
            (query, search) => query.Where(x => x.ReasonId.ToString() == search.ReasonId)),
        new SearchFieldMutator<CommentReport, CommentReportSearchModel>(
            search => !string.IsNullOrWhiteSpace(search.CommentId),
            (query, search) => query.Where(x => x.CommentId.ToString() == search.CommentId)),
        new SearchFieldMutator<CommentReport, CommentReportSearchModel>(
            search => !string.IsNullOrWhiteSpace(search.ReporterId),
            (query, search) => query.Where(x => x.ReporterId.ToString() == search.ReporterId)),
        new SearchFieldMutator<CommentReport, CommentReportSearchModel>(
            search => search.Status.HasValue,
            (query, search) => query.Where(x => x.Status == search.Status!.Value)),
        new SearchFieldMutator<CommentReport, CommentReportSearchModel>(
            search => search.OrderBy is not { Length: 0 },
            (query, search) =>
            {
                IOrderedQueryable<CommentReport> ordered = search.OrderBy[0] switch
                {
                    CommentReportOrderBy.CreationTime => query.OrderBy(x => x.CreationTime),
                    CommentReportOrderBy.CreationTimeDesc => query.OrderByDescending(x => x.CreationTime),
                    _ => query.OrderByDescending(x => x.CreationTime)
                };
                return search.OrderBy.Skip(1).Aggregate(ordered, (current, orderBy) => orderBy switch
                {
                    CommentReportOrderBy.CreationTime => current.ThenBy(x => x.CreationTime),
                    CommentReportOrderBy.CreationTimeDesc => current.ThenByDescending(x => x.CreationTime),
                    _ => current
                });
            })
    ];

    [NonAction]
    public override ActionResult<CommentReportModel> Post(CommentReportModel input)
    {
        return base.Post(input);
    }

    [HttpPost("{commentId}")]
    public ActionResult<CommentReportModel> Post(ulong commentId, CommentReportModel input)
    {
        input.CommentId = commentId.ToString();
        input.ReporterId = User.GetIdString();
        input.Status = ReportStatus.Pending;
        Logger.LogInformation("Creating comic report: {Input}", JsonConvert.SerializeObject(input));
        return base.Post(input);
    }

    [HttpGet("reasons")]
    public ActionResult<IEnumerable<ReportReasonModel>> GetReasons()
    {
        List<CommentReportReason> result = DbContext.CommentReportReasons.ToList();
        return Ok(Mapper.Map<IEnumerable<ReportReasonModel>>(result));
    }
}