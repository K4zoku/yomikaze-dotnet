using Yomikaze.Application.Data.Repos;
using Yomikaze.Application.Helpers.API;

namespace Yomikaze.API.Main.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Administrator,Publisher")]
public class ComicsController(
    DbContext dbContext,
    IMapper mapper,
    IDistributedCache cache,
    ILogger<ComicsController> logger)
    : CrudControllerBase<Comic, ComicModel>(dbContext, mapper, new ComicRepository(dbContext), cache, logger)
{
    
    private ComicRepository ComicRepository => (ComicRepository)Repository;
    
    public override ActionResult<PagedResult> List(PaginationModel pagination)
    {
        string keyName = $"{KeyPrefix}:list({pagination.Page}, {pagination.Size})";
        if (Cache.TryGet(keyName, out PagedResult? cachedModels))
        {
            return Ok(cachedModels);
        }

        var query = ComicRepository.QueryWithExtras();
        PagedResult paged = GetPaged(query, pagination);

        Cache.SetInBackground(keyName, paged,
            new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) });
        return paged;
    }
    
    private IList<SearchFieldMutator<Comic, ComicSearchModel>> SearchFieldMutators { get; } = new List<SearchFieldMutator<Comic, ComicSearchModel>>
    {
        new(searchModel => !string.IsNullOrWhiteSpace(searchModel.Name),
            (query, search) => query.Where(comic => comic.Name.ToLower().Contains(search.Name!) ||
                                                        comic.Aliases.Any(alias => alias.ToLower().Contains(search.Name!)))),
        new(searchModel => !string.IsNullOrWhiteSpace(searchModel.Publisher),
            (query, search) => query.Where(comic => comic.Publisher.Name.ToLower().Contains(search.Publisher!))),
        new(searchModel => searchModel.IncludeTags.Length != 0,
            (query, search) => search.InclusionMode == LogicalOperator.And ?
                query.Where(comic => search.IncludeTags.All(searchTag => comic.Tags.Any(tag => tag.Name.ToLower().Contains(searchTag.ToLower()) || tag.Id.ToString() == searchTag))) : 
                query.Where(comic => comic.Tags.Any(tag => search.IncludeTags.Any(searchTag => tag.Name.ToLower().Contains(searchTag.ToLower()) || tag.Id.ToString() == searchTag)))),
        new(searchModel => searchModel.ExcludeTags.Length != 0,
            (query, search) => search.ExclusionMode == LogicalOperator.And ?
                query.Where(comic => search.ExcludeTags.All(searchTag => comic.Tags.All(tag => tag.Name.ToLower().Contains(searchTag.ToLower()) || tag.Id.ToString() == searchTag))) : 
                query.Where(comic => comic.Tags.All(tag => search.ExcludeTags.Any(searchTag => tag.Name.ToLower().Contains(searchTag.ToLower()) || tag.Id.ToString() == searchTag)))),
        new(searchModel => searchModel.Authors.Length != 0,
            (query, search) => query.Where(comic => search.Authors.All(searchAuthor => comic.Authors.Any(author => author.ToLower().Contains(searchAuthor.ToLower()))))),
        new(searchModel => searchModel.FromPublicationDate.HasValue,
            (query, search) => query.Where(comic => comic.PublicationDate >= search.FromPublicationDate)),
        new(searchModel => searchModel.ToPublicationDate.HasValue,
            (query, search) => query.Where(comic => comic.PublicationDate <= search.ToPublicationDate)),
        new(searchModel => searchModel.Status.HasValue,
            (query, search) => query.Where(comic => search.Status == comic.Status)),
        new(searchModel => searchModel.FromTotalChapters.HasValue,
            (query, search) => query.Where(comic => comic.TotalChapters >= search.FromTotalChapters)),
        new(searchModel => searchModel.ToTotalChapters.HasValue,
            (query, search) => query.Where(comic => comic.TotalChapters <= search.ToTotalChapters)),
        new(searchModel => searchModel.FromTotalViews.HasValue,
            (query, search) => query.Where(comic => comic.TotalViews >= search.FromTotalViews)),
        new(searchModel => searchModel.ToTotalViews.HasValue,
            (query, search) => query.Where(comic => comic.TotalViews <= search.ToTotalViews)),
        new(searchModel => searchModel.FromAverageRating.HasValue,
            (query, search) => query.Where(comic => comic.AverageRating >= search.FromAverageRating)),
        new(searchModel => searchModel.ToAverageRating.HasValue,
            (query, search) => query.Where(comic => comic.AverageRating <= search.ToAverageRating)),
        new(searchModel => searchModel.FromTotalFollows.HasValue,
            (query, search) => query.Where(comic => comic.TotalFollows >= search.FromTotalFollows)),
        new(searchModel => searchModel.ToTotalFollows.HasValue,
            (query, search) => query.Where(comic => comic.TotalFollows <= search.ToTotalFollows))
    };
    
    [HttpGet("[action]")]
    public ActionResult<PagedResult> Search([FromQuery] ComicSearchModel searchModel)
    {
        var queryable = ComicRepository.QueryWithExtras();
        queryable = SearchFieldMutators.Aggregate(queryable, (current, mutator) => mutator.Apply(searchModel, current));
        return Ok(GetPaged(queryable, searchModel));
    }

    public override ActionResult<ComicModel> Post(
        [Bind($"{nameof(ComicModel.Name)},{nameof(ComicModel.Description)},{nameof(ComicModel.Cover)},{nameof(ComicModel.Banner)},{nameof(ComicModel.PublicationDate)},{nameof(ComicModel.Authors)},{nameof(ComicModel.Status)},{nameof(ComicModel.TagIds)}")]
        ComicModel input)
    {
        input.PublisherId = User.GetIdString();
        return base.Post(input);
    }
}
