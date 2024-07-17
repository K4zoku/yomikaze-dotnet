using Yomikaze.Application.Helpers.API;

namespace Yomikaze.API.Main.Controllers;

[ApiController]
[Route("reports/comics")]
public class ComicReportController(
    ComicReportRepository repository,
    IMapper mapper,
    ILogger<SearchControllerBase<ComicReport, ComicReportModel, ComicReportRepository, ComicReportSearchModel>> logger)
    : SearchControllerBase<ComicReport, ComicReportModel, ComicReportRepository, ComicReportSearchModel>(repository,
        mapper, logger)
{
    protected override IList<SearchFieldMutator<ComicReport, ComicReportSearchModel>> SearchFieldMutators { get; } =
    [
        new SearchFieldMutator<ComicReport, ComicReportSearchModel>(
            search => !string.IsNullOrWhiteSpace(search.ReportCategoryId),
            (query, search) => query.Where(x => x.CategoryId.ToString() == search.ReportCategoryId)),
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
    
    [HttpPost("/comics/{comicId}/reports")]
    public ActionResult<ComicReportModel> Post(ulong comicId, ComicReportModel input)
    {
        input.ComicId = comicId.ToString();
        input.ReporterId = User.GetIdString();
        input.Status = ReportStatus.Pending;
        return base.Post(input);
    }
}