using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;
using Yomikaze.Application.Helpers.API;
using Yomikaze.Infrastructure.Context;

namespace Yomikaze.API.Main.Controllers;

[ApiController]
[Route("/reports/profile")]
public class ProfileReportController(
    YomikazeDbContext dbContext,
    ProfileReportRepository repository,
    IMapper mapper,
    ILogger<ProfileReportController> logger)
    : SearchControllerBase<ProfileReport, ProfileReportModel, ProfileReportRepository, ProfileReportSearchModel>(repository,
        mapper, logger)
{
    
    private YomikazeDbContext DbContext { get; } = dbContext;
    
    protected override IList<SearchFieldMutator<ProfileReport, ProfileReportSearchModel>> SearchFieldMutators { get; } =
    [
        new SearchFieldMutator<ProfileReport, ProfileReportSearchModel>(
            search => !string.IsNullOrWhiteSpace(search.ReasonId),
            (query, search) => query.Where(x => x.ReasonId.ToString() == search.ReasonId)),
        new SearchFieldMutator<ProfileReport, ProfileReportSearchModel>(
            search => !string.IsNullOrWhiteSpace(search.ProfileId),
            (query, search) => query.Where(x => x.ProfileId.ToString() == search.ProfileId)),
        new SearchFieldMutator<ProfileReport, ProfileReportSearchModel>(
            search => !string.IsNullOrWhiteSpace(search.ReporterId),
            (query, search) => query.Where(x => x.ReporterId.ToString() == search.ReporterId)),
        new SearchFieldMutator<ProfileReport, ProfileReportSearchModel>(
            search => search.Status.HasValue,
            (query, search) => query.Where(x => x.Status == search.Status!.Value)),
        new SearchFieldMutator<ProfileReport, ProfileReportSearchModel>(
            search => search.OrderBy is not { Length: 0 },
            (query, search) =>
            {
                var ordered = search.OrderBy[0] switch
                {
                    ProfileReportOrderBy.CreationTime => query.OrderBy(x => x.CreationTime),
                    ProfileReportOrderBy.CreationTimeDesc => query.OrderByDescending(x => x.CreationTime),
                    _ => query.OrderByDescending(x => x.CreationTime)
                };
                return search.OrderBy.Skip(1).Aggregate(ordered, (current, orderBy) => orderBy switch
                {
                    ProfileReportOrderBy.CreationTime => current.ThenBy(x => x.CreationTime),
                    ProfileReportOrderBy.CreationTimeDesc => current.ThenByDescending(x => x.CreationTime),
                    _ => current
                });
            })
    ];

    [NonAction]
    public override ActionResult<ProfileReportModel> Post(ProfileReportModel input)
    {
        return base.Post(input);
    }
    
    [HttpPost("{profileId}")]
    public ActionResult<ProfileReportModel> Post(ulong profileId, ProfileReportModel input)
    {
        input.ProfileId = profileId.ToString();
        input.ReporterId = User.GetIdString();
        input.Status = ReportStatus.Pending;
        Logger.LogInformation("Creating comic report: {Input}", JsonConvert.SerializeObject(input));
        return base.Post(input);
    }
    
    [HttpGet("profile/reasons")]
    public ActionResult<IEnumerable<ReportReasonModel>> GetReasons()
    {
        var result = DbContext.ProfileReportReasons.ToList();
        return Ok(Mapper.Map<IEnumerable<ReportReasonModel>>(result));
    }
}