using System.Net;
using Yomikaze.Application.Data.Repos;
using Yomikaze.Application.Helpers;
using Yomikaze.Application.Helpers.API;
using Yomikaze.Domain.Entities.Weak;

namespace Yomikaze.API.Main.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Administrator, Publisher")]
public class ComicsController(
    DbContext dbContext,
    IMapper mapper,
    IDistributedCache cache,
    ILogger<ComicsController> logger)
    : CrudControllerBase<Comic, ComicModel>(dbContext, mapper, new ComicRepository(dbContext), cache, logger)
{
    public override ActionResult<PagedResult> List(PaginationModel pagination)
    {
        string keyName = $"{KeyPrefix}:list({pagination.Page}, {pagination.Size})";
        if (Cache.TryGet(keyName, out PagedResult? cachedModels))
        {
            return Ok(cachedModels);
        }
        PagedResult paged = GetPaged(Repository.Query(), pagination);

        paged.Results = paged.Results.Select(QueryExtraComicData).ToList();
        
        Cache.SetInBackground(keyName, paged, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) });
        return paged;
    }

    public override ActionResult<ComicModel> Get(ulong key)
    {
        string keyName = KeyPrefix + key;

        if (Cache.TryGet(keyName, out ComicModel? cachedModels))
        {
            return Ok(cachedModels);
        }

        Comic? entity = Repository.Get(key);

        if (entity == null)
        {
            throw new HttpResponseException(HttpStatusCode.NotFound, "Not found");
        }

        ComicModel model = Mapper.Map<ComicModel>(entity);
        model = QueryExtraComicData(model);
        
        Cache.SetInBackground(keyName, model, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) });
        return Ok(model);
    }

    private ComicModel QueryExtraComicData(ComicModel comic)
    {
        IList<int> ratings = DbContext.Set<ComicRating>().Where(r => r.ComicId.ToString() == comic.Id).Select(r => r.Rating).ToList();
        IList<int> views = DbContext.Set<Chapter>().Where(c => c.ComicId.ToString() == comic.Id).Select(c => c.Views).ToList();
        
        comic.TotalRatings = ratings.Count;
        comic.AverageRating = comic.TotalRatings > 0 ? ratings.Average() : 0;
        comic.TotalChapters = views.Count;
        comic.TotalViews = views.Sum();
        
        return comic;
    }

    public override ActionResult<ComicModel> Post([Bind("Name,Description,Cover,Banner,PublicationDate,Authors,Status,TagIds")] ComicModel input)
    {
        input.PublisherId = User.GetIdString();
        return base.Post(input);
    }
}