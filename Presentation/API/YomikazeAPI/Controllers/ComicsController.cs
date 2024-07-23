using System.Diagnostics.CodeAnalysis;
using Yomikaze.API.Main.Helpers;
using Yomikaze.Application.Helpers.API;
using Yomikaze.Domain.Entities.Weak;
using Yomikaze.Infrastructure.Context;
using static Newtonsoft.Json.JsonConvert;

namespace Yomikaze.API.Main.Controllers;

[ApiController]
[Route("[controller]")]
[SuppressMessage("Performance",
    "CA1862:Use the \'StringComparison\' method overloads to perform case-insensitive string comparisons")]
public partial class ComicsController(
    YomikazeDbContext dbContext,
    ComicRepository repository,
    ChapterRepository chapterRepository,
    HistoryRepository historyRepository,
    LibraryRepository libraryRepository,
    LibraryCategoryRepository libraryCategoryRepository,
    IMapper mapper,
    IDistributedCache cache,
    ILogger<ComicsController> logger)
    : CrudControllerBase<Comic, ComicModel, ComicRepository>(repository, mapper, cache, logger)
{
    private YomikazeDbContext DbContext { get; } = dbContext;
    private ChapterRepository ChapterRepository { get; } = chapterRepository;

    private HistoryRepository HistoryRepository { get; } = historyRepository;
    
    private LibraryRepository LibraryRepository { get; } = libraryRepository;
    
    private LibraryCategoryRepository LibraryCategoryRepository { get; } = libraryCategoryRepository;

    private IList<SearchFieldMutator<Comic, ComicSearchModel>> SearchFieldMutators { get; } =
        new List<SearchFieldMutator<Comic, ComicSearchModel>>
        {
            new(searchModel => !string.IsNullOrWhiteSpace(searchModel.Name),
                (query, search) => query.Where(comic => comic.Name.ToLower().Contains(search.Name!) ||
                                                        comic.Aliases.Any(alias =>
                                                            alias.ToLower().Contains(search.Name!)))),
            new(searchModel => !string.IsNullOrWhiteSpace(searchModel.Publisher),
                (query, search) => query.Where(comic => comic.Publisher.Name.ToLower().Contains(search.Publisher!))),
            new(searchModel => searchModel.PublisherId.HasValue,
                (query, search) => query.Where(comic => comic.PublisherId == search.PublisherId)),
            new(searchModel => searchModel.IncludeTags != null && searchModel.IncludeTags.Length != 0,
                (query, search) => search.InclusionMode == LogicalOperator.Or
                    ? query.Where(comic => comic.Tags.Any(tag => (search.IncludeTags!.Any(searchTag =>
                        tag.Name.ToLower().Contains(searchTag.ToLower()) || tag.Id.ToString() == searchTag))))
                    : query.Where(comic => Array.TrueForAll(search.IncludeTags!, searchTag => comic.Tags.Any(tag =>
                        tag.Name.ToLower().Contains(searchTag.ToLower()) || tag.Id.ToString() == searchTag)))),
            new(searchModel => searchModel.ExcludeTags != null && searchModel.ExcludeTags.Length != 0,
                (query, search) => search.ExclusionMode == LogicalOperator.And
                    ? query.Where(comic => Array.TrueForAll(search.ExcludeTags!, searchTag => comic.Tags.All(tag =>
                        tag.Name.ToLower().Contains(searchTag.ToLower()) || tag.Id.ToString() == searchTag)))
                    : query.Where(comic => comic.Tags.All(tag => search.ExcludeTags!.Any(searchTag =>
                        tag.Name.ToLower().Contains(searchTag.ToLower()) || tag.Id.ToString() == searchTag)))),
            new(searchModel => searchModel.Authors != null && searchModel.Authors.Length != 0,
                (query, search) => query.Where(comic => search.Authors!.Any(searchAuthor =>
                    comic.Authors.Any(author => author.ToLower().Contains(searchAuthor.ToLower()))))),
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
                (query, search) => query.Where(comic => comic.TotalFollows <= search.ToTotalFollows)),
            new(searchModel => searchModel.OrderBy != null && searchModel.OrderBy.Length != 0,
                (query, search) =>
                {
                    ComicOrderBy firstMutator = search.OrderBy![0];
                    IOrderedQueryable<Comic> orderedQuery = firstMutator switch
                    {
                        ComicOrderBy.Name => query.OrderBy(comic => comic.Name),
                        ComicOrderBy.NameDesc => query.OrderByDescending(comic => comic.Name),
                        ComicOrderBy.PublicationDate => query.OrderBy(comic => comic.PublicationDate),
                        ComicOrderBy.PublicationDateDesc => query.OrderByDescending(comic => comic.PublicationDate),
                        ComicOrderBy.CreationTime => query.OrderBy(comic => comic.CreationTime),
                        ComicOrderBy.CreationTimeDesc => query.OrderByDescending(comic => comic.CreationTime),
                        ComicOrderBy.LastModified => query.OrderBy(comic => comic.LastModified),
                        ComicOrderBy.LastModifiedDesc => query.OrderByDescending(comic => comic.LastModified),
                        ComicOrderBy.TotalChapters => query.OrderBy(comic => comic.TotalChapters),
                        ComicOrderBy.TotalChaptersDesc => query.OrderByDescending(comic => comic.TotalChapters),
                        ComicOrderBy.TotalViews => query.OrderBy(comic => comic.TotalViews),
                        ComicOrderBy.TotalViewsDesc => query.OrderByDescending(comic => comic.TotalViews),
                        ComicOrderBy.AverageRating => query.OrderBy(comic => comic.AverageRating),
                        ComicOrderBy.AverageRatingDesc => query.OrderByDescending(comic => comic.AverageRating),
                        ComicOrderBy.TotalRatings => query.OrderBy(comic => comic.TotalRatings),
                        ComicOrderBy.TotalRatingsDesc => query.OrderByDescending(comic => comic.TotalRatings),
                        ComicOrderBy.TotalFollows => query.OrderBy(comic => comic.TotalFollows),
                        ComicOrderBy.TotalFollowsDesc => query.OrderByDescending(comic => comic.TotalFollows),
                        ComicOrderBy.TotalComments => query.OrderBy(comic => comic.TotalComments),
                        ComicOrderBy.TotalCommentsDesc => query.OrderByDescending(comic => comic.TotalComments),
                        _ => query.OrderByDescending(comic => comic.Id)
                    };
                    return search.OrderBy!.Skip(1).Aggregate(orderedQuery, (ordered, orderBy) => orderBy switch
                    {
                        ComicOrderBy.Name => ordered.ThenBy(comic => comic.Name),
                        ComicOrderBy.NameDesc => ordered.ThenByDescending(comic => comic.Name),
                        ComicOrderBy.PublicationDate => ordered.ThenBy(comic => comic.PublicationDate),
                        ComicOrderBy.PublicationDateDesc => ordered.ThenByDescending(comic => comic.PublicationDate),
                        ComicOrderBy.CreationTime => ordered.ThenBy(comic => comic.CreationTime),
                        ComicOrderBy.CreationTimeDesc => ordered.ThenByDescending(comic => comic.CreationTime),
                        ComicOrderBy.LastModified => ordered.ThenBy(comic => comic.LastModified),
                        ComicOrderBy.LastModifiedDesc => ordered.ThenByDescending(comic => comic.LastModified),
                        ComicOrderBy.TotalChapters => ordered.ThenBy(comic => comic.TotalChapters),
                        ComicOrderBy.TotalChaptersDesc => ordered.ThenByDescending(comic => comic.TotalChapters),
                        ComicOrderBy.TotalViews => ordered.ThenBy(comic => comic.TotalViews),
                        ComicOrderBy.TotalViewsDesc => ordered.ThenByDescending(comic => comic.TotalViews),
                        ComicOrderBy.AverageRating => ordered.ThenBy(comic => comic.AverageRating),
                        ComicOrderBy.AverageRatingDesc => ordered.ThenByDescending(comic => comic.AverageRating),
                        ComicOrderBy.TotalRatings => ordered.ThenBy(comic => comic.TotalRatings),
                        ComicOrderBy.TotalRatingsDesc => ordered.ThenByDescending(comic => comic.TotalRatings),
                        ComicOrderBy.TotalFollows => ordered.ThenBy(comic => comic.TotalFollows),
                        ComicOrderBy.TotalFollowsDesc => ordered.ThenByDescending(comic => comic.TotalFollows),
                        ComicOrderBy.TotalComments => ordered.ThenBy(comic => comic.TotalComments),
                        ComicOrderBy.TotalCommentsDesc => ordered.ThenByDescending(comic => comic.TotalComments),
                        _ => ordered
                    });
                })
        };

    private static List<string> ListCacheKeys { get; } = [];
    private static object ListCacheKeyLock { get; } = new();
    
    [NonAction]
    public override ActionResult<PagedList<ComicModel>> List(PaginationModel pagination)
    {
        return NotFound();
    }
    

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<PagedList<ComicModel>>> List([FromQuery] ComicSearchModel search,
        [FromQuery] PaginationModel pagination)
    {
        string key = $"{CacheKeyPrefix}{nameof(List)}:{SerializeObject(search)}:[{pagination.Page},{pagination.Size}]";
        bool isAuthorized = User.Identity is { IsAuthenticated: true };
        if (isAuthorized)
        {
            key += $":{User.GetId()}";
        }
        var result = await Cache.GetOrSetAsync(key, valueFactory: () =>
        {
            lock (ListCacheKeyLock)
            {
                ListCacheKeys.Add(key);
            }

            IQueryable<Comic> queryable = Repository.QueryWithExtras();
            queryable = SearchFieldMutators.Aggregate(queryable, (current, mutator) => mutator.Apply(search, current));
            PagedList<ComicModel> paged = GetPaged(queryable, pagination);
            if (!isAuthorized) return paged;
            ulong userId = User.GetId();
            foreach (var model in paged.Results)
            {
                if (!ulong.TryParse(model.Id, out ulong comicId))
                {
                    continue;
                }
                model.IsFollowing = LibraryRepository.IsFollowing(userId, comicId);
            }
            return paged;
        }, logger: Logger);

        return result;
    }
    
    [HttpGet("/management")]
    [Authorize(Roles = "Administrator,Publisher")]
    public async Task<ActionResult<PagedList<ComicModel>>> ListForManagement([FromQuery] ComicSearchModel search,
        [FromQuery] PaginationModel pagination)
    {
        search.PublisherId = User.GetId();
        return await List(search, pagination);
    }
    
    private ICollection<ComicModel> GetRankingByTime(DateTimeOffset start, DateTimeOffset end)
    {
        IQueryable<Comic> queryable = Repository.QueryWithExtras();
        var topRead = queryable
            .Select(comic => new {
                    Comic = comic,
                    Views = DbContext.GetComicViewsResult(comic.Id, start, end).Sum(r => (int) r.Views)
                }
            )
            .OrderByDescending(r => r.Views)
            .Take(10)
            .ToList();
        var models = Mapper.Map<ICollection<ComicModel>>(topRead.Select(r => r.Comic));
        foreach (var model in models)
        {
            model.TotalViews = topRead.Where(r => r.Comic.Id.ToString() == model.Id).Sum(r => r.Views);
        }
        return models;
    }
    
    private static DateTimeOffset RemoveTime(DateTimeOffset dateTime)
    {
        return dateTime.Subtract(TimeSpan.FromHours(dateTime.Hour))
            .Subtract(TimeSpan.FromMinutes(dateTime.Minute))
            .Subtract(TimeSpan.FromSeconds(dateTime.Second))
            .Subtract(TimeSpan.FromMilliseconds(dateTime.Millisecond));
    }
    
    [HttpGet("ranking/weekly")]
    [AllowAnonymous]
    public async Task<ActionResult<ICollection<ComicModel>>> GetTopReadByWeek()
    {
        string key = $"{CacheKeyPrefix}{nameof(GetTopReadByWeek)}";
        var now = RemoveTime(DateTimeOffset.UtcNow);
        
        var result = await Cache.GetOrSetAsync(key, 
            valueFactory: () => 
                Mapper.Map<ICollection<ComicModel>>(
                    GetRankingByTime(
                        now.Subtract(TimeSpan.FromDays((int) now.DayOfWeek)),
                        now
                    )
                ), logger: Logger);

        return Ok(result);
    }
    
    [HttpGet("ranking/monthly")]
    [AllowAnonymous]
    public async Task<ActionResult<ICollection<ComicModel>>> GetTopReadByMonth()
    {
        string key = $"{CacheKeyPrefix}{nameof(GetTopReadByMonth)}";
        var result = await Cache.GetOrSetAsync(key, valueFactory: () => 
            Mapper.Map<ICollection<ComicModel>>(
                GetRankingByTime(
                    RemoveTime(DateTimeOffset.UtcNow).Subtract(TimeSpan.FromDays(DateTimeOffset.UtcNow.Day)),
                    DateTimeOffset.UtcNow
                )
            ), logger: Logger);

        return Ok(result);
    }
    
    [HttpGet("ranking/yearly")]
    [AllowAnonymous]
    public async Task<ActionResult<ICollection<ComicModel>>> GetTopReadByYear()
    {
        string key = $"{CacheKeyPrefix}{nameof(GetTopReadByYear)}";
        var result = await Cache.GetOrSetAsync(key, valueFactory: () => 
            Mapper.Map<ICollection<ComicModel>>(
                GetRankingByTime(
                    RemoveTime(DateTimeOffset.UtcNow).Subtract(TimeSpan.FromDays(DateTimeOffset.UtcNow.DayOfYear)),
                    DateTimeOffset.UtcNow
                )
            ), logger: Logger);

        return Ok(result);
    }
        
        
    [HttpPost]
    [Authorize(Roles = "Administrator,Publisher")]
    public override ActionResult<ComicModel> Post(
        [Bind(
            $"{nameof(ComicModel.Name)},{nameof(ComicModel.Description)},{nameof(ComicModel.Cover)},{nameof(ComicModel.Banner)},{nameof(ComicModel.PublicationDate)},{nameof(ComicModel.Authors)},{nameof(ComicModel.Status)},{nameof(ComicModel.TagIds)}")]
        ComicModel input)
    {
        input.PublisherId = User.GetIdString();
        return base.Post(input);
    }
    
    [HttpGet("{key}")]
    [AllowAnonymous]
    public override ActionResult<ComicModel> Get(ulong key)
    {
        bool isAuthorized = User.Identity is { IsAuthenticated: true };
        
        Comic? entity = Repository.Get(key);
        if (entity == null)
        {
            Logger.LogWarning("Entity with key {Key} not found", key);
            return NotFound();
        }
        ComicModel model = Mapper.Map<ComicModel>(entity);
        if (isAuthorized)
        {
            model.IsFollowing = LibraryRepository.IsFollowing(User.GetId(), entity.Id);
            model.MyRating = entity.Ratings.FirstOrDefault(r => r.UserId == User.GetId())?.Rating;
            model.IsRated = model.MyRating != null;
            model.IsRead = HistoryRepository.Exists(User.GetId(), entity);
        }
        ModelWriteOnlyProperties.ForEach(x => x.SetValue(model, default));
        return Ok(model);
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "Administrator,Publisher")]
    public ActionResult<ICollection<ComicModel>> Batch([FromBody] ICollection<ComicModel> comics)
    {
        Comic[] entities = Mapper.Map<Comic[]>(comics.Select(comic =>
        {
            comic.PublisherId = User.GetIdString();
            return comic;
        }));
        Repository.Add(entities);
        return Ok(Mapper.Map<ICollection<ComicModel>>(entities));
    }
    
    [Authorize]
    [HttpPut("{key}/rate")]
    public ActionResult Rate(ulong key, [FromBody] ComicRatingModel rating)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        ulong userId = User.GetId();
        Comic? comic = Repository.Get(key);
        if (comic == null)
        {
            return NotFound();
        }
        
        ComicRating? existingRating = comic.Ratings.FirstOrDefault(r => r.UserId == userId);
        
        if (existingRating != null)
        {
            existingRating.Rating = rating.Rating!.Value;
            Repository.Update(comic);   
            ClearCacheForUser(key, userId);
            return Ok();
        }

        ComicRating newRating = new()
        {
            UserId = userId,
            ComicId = key,
            Rating = rating.Rating!.Value
        };
        
        comic.Ratings.Add(newRating);
        Repository.Update(comic);
        ClearCacheForUser(key, userId);
        return Ok();
    }
    
    private void ClearCacheForUser(ulong key, ulong userId)
    {
        Task.Run(() =>
        {
            Cache.Remove($"{CacheKeyPrefix}{key}:{userId}");
            lock (ListCacheKeyLock)
            {
                ListCacheKeys.ForEach(k => Cache.Remove(k));
                ListCacheKeys.Clear();
            }
        });
    }
    
    
}