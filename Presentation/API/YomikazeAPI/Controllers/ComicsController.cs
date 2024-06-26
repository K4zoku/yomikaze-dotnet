using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
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

    private ChapterRepository ChapterRepository { get; } = new(dbContext);

    private HistoryRepository HistoryRepository { get; } = new(dbContext);

    [NonAction]
    public override ActionResult<PagedResult> List(PaginationModel pagination)
    {
        return NotFound();
    }

    private IList<SearchFieldMutator<Comic, ComicSearchModel>> SearchFieldMutators { get; } =
        new List<SearchFieldMutator<Comic, ComicSearchModel>>
        {
            new(searchModel => !string.IsNullOrWhiteSpace(searchModel.Name),
                (query, search) => query.Where(comic => comic.Name.ToLower().Contains(search.Name!) ||
                                                        comic.Aliases.Any(alias =>
                                                            alias.ToLower().Contains(search.Name!)))),
            new(searchModel => !string.IsNullOrWhiteSpace(searchModel.Publisher),
                (query, search) => query.Where(comic => comic.Publisher.Name.ToLower().Contains(search.Publisher!))),
            new(searchModel => searchModel.IncludeTags.Length != 0,
                (query, search) => search.InclusionMode == LogicalOperator.And
                    ? query.Where(comic => search.IncludeTags.All(searchTag => comic.Tags.Any(tag =>
                        tag.Name.ToLower().Contains(searchTag.ToLower()) || tag.Id.ToString() == searchTag)))
                    : query.Where(comic => comic.Tags.Any(tag => search.IncludeTags.Any(searchTag =>
                        tag.Name.ToLower().Contains(searchTag.ToLower()) || tag.Id.ToString() == searchTag)))),
            new(searchModel => searchModel.ExcludeTags.Length != 0,
                (query, search) => search.ExclusionMode == LogicalOperator.And
                    ? query.Where(comic => search.ExcludeTags.All(searchTag => comic.Tags.All(tag =>
                        tag.Name.ToLower().Contains(searchTag.ToLower()) || tag.Id.ToString() == searchTag)))
                    : query.Where(comic => comic.Tags.All(tag => search.ExcludeTags.Any(searchTag =>
                        tag.Name.ToLower().Contains(searchTag.ToLower()) || tag.Id.ToString() == searchTag)))),
            new(searchModel => searchModel.Authors.Length != 0,
                (query, search) => query.Where(comic => search.Authors.All(searchAuthor =>
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
            new(searchModel => searchModel.OrderBy.Length != 0,
            (query, search) =>
            {
                var firstMutator = search.OrderBy.First();
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
                return search.OrderBy.Skip(1).Aggregate(orderedQuery, (ordered, orderBy) => orderBy switch
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
        

    [HttpGet]
    [Authorize] // Only authenticated users (any role) can access this endpoint
    public ActionResult<PagedResult> Search([FromQuery] ComicSearchModel searchModel)
    {
        var queryable = ComicRepository.QueryWithExtras();
        queryable = SearchFieldMutators.Aggregate(queryable, (current, mutator) => mutator.Apply(searchModel, current));
        return Ok(GetPaged(queryable, searchModel));
    }

    public override ActionResult<ComicModel> Post(
        [Bind(
            $"{nameof(ComicModel.Name)},{nameof(ComicModel.Description)},{nameof(ComicModel.Cover)},{nameof(ComicModel.Banner)},{nameof(ComicModel.PublicationDate)},{nameof(ComicModel.Authors)},{nameof(ComicModel.Status)},{nameof(ComicModel.TagIds)}")]
        ComicModel input)
    {
        input.PublisherId = User.GetIdString();
        return base.Post(input);
    }

    [HttpPost("[action]")]
    public ActionResult<ICollection<ComicModel>> Batch([FromBody] ICollection<ComicModel> comics)
    {
        Comic[] entities = Mapper.Map<Comic[]>(comics);
        Repository.Add(entities);
        return Ok(Mapper.Map<ICollection<ComicModel>>(entities));
    }

    [HttpPost("{key}/chapters")]
    public ActionResult<ChapterModel> PostChapter(ulong key, ChapterModel model)
    {
        Comic? comic = ComicRepository.Get(key);
        if (comic == null)
        {
            return NotFound();
        }

        if (comic.PublisherId != User.GetId())
        {
            return Forbid();
        }

        Chapter chapter = Mapper.Map<Chapter>(model);
        chapter.ComicId = key;

        ChapterRepository.Add(chapter);

        return CreatedAtAction(nameof(GetChapter), new { key, number = chapter.Number },
            Mapper.Map<ChapterModel>(chapter));
    }


    [HttpGet("{key}/chapters")]
    [AllowAnonymous]
    public ActionResult<ICollection<ChapterModel>> GetChapters(ulong key)
    {
        Comic? entity = Repository.Query().Include(c => c.Chapters).FirstOrDefault(c => c.Id == key);
        if (entity == null)
        {
            return NotFound();
        }

        return Ok(Mapper.Map<ICollection<ChapterModel>>(entity.Chapters));
    }

    [HttpGet("{key}/chapters/{number}")]
    [AllowAnonymous]
    public ActionResult<ChapterModel> GetChapter(ulong key, int number)
    {
        Chapter? chapter = ChapterRepository.GetByComicIdAndIndex(key.ToString(), number);
        if (chapter == null)
        {
            return NotFound();
        }

        if (User.Identity?.IsAuthenticated ?? false)
        {
            HistoryRepository.Add(new HistoryRecord() { ChapterId = chapter.Id, UserId = User.GetId() });
        }

        return Ok(Mapper.Map<ChapterModel>(chapter));
    }

    [HttpPatch("{key}/chapters/{number}")]
    public ActionResult<ChapterModel> UpdateChapter(ulong key, int number,
        JsonPatchDocument<ChapterModel> patchDocument)
    {
        Comic? comic = ComicRepository.Get(key);

        if (comic == null)
        {
            return NotFound();
        }

        if (comic.PublisherId != User.GetId())
        {
            return Forbid();
        }

        Chapter? chapter = ChapterRepository.GetByComicIdAndIndex(key.ToString(), number);

        if (chapter == null)
        {
            return NotFound();
        }

        ChapterModel model = Mapper.Map<ChapterModel>(chapter);
        patchDocument.ApplyTo(model);
        Mapper.Map(model, chapter);
        ChapterRepository.Update(chapter);
        return Ok(Mapper.Map<ChapterModel>(chapter));
    }

    [HttpDelete("{key}/chapters/{number}")]
    public ActionResult<ChapterModel> DeleteChapter(ulong key, int number,
        JsonPatchDocument<ChapterModel> patchDocument)
    {
        Comic? comic = ComicRepository.Get(key);

        if (comic == null)
        {
            return NotFound();
        }

        if (comic.PublisherId != User.GetId())
        {
            return Forbid();
        }

        Chapter? chapter = ChapterRepository.GetByComicIdAndIndex(key.ToString(), number);

        if (chapter == null)
        {
            return NotFound();
        }

        ChapterRepository.Delete(chapter);
        return NoContent();
    }
}