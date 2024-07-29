using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using Yomikaze.Application.Helpers.API;

namespace Yomikaze.API.Main.Controllers;

[Authorize]
[Route("[controller]")]
[ApiController]
public class LibraryController(
    LibraryRepository repository,
    IMapper mapper,
    ILogger<LibraryController> logger)
    : SearchControllerBase<LibraryEntry, LibraryEntryModel, LibraryRepository, LibrarySearchModel>(repository, mapper,
        logger)
{
    protected override IList<SearchFieldMutator<LibraryEntry, LibrarySearchModel>> SearchFieldMutators { get; } =
        new List<SearchFieldMutator<LibraryEntry, LibrarySearchModel>>
        {
#pragma warning disable CA1862
            new(search => !string.IsNullOrWhiteSpace(search.Name),
                (query, search) => query.Where(x => x.Comic.Name.ToLower().Contains(search.Name!.ToLower()))),
#pragma warning restore CA1862
            new(search => search.CategoryId != null,
                (query, search) => query.Where(x => x.Categories.Any(c => c.Id == search.CategoryId))),
            new(search => search.HasNoCategory.HasValue,
                (query, search) => search.HasNoCategory!.Value ? query.Where(x => x.Categories.Count == 0) : query),
            new(search => search.OrderBy.Length > 0, (query, search) =>
            {
                IOrderedQueryable<LibraryEntry> ordered = search.OrderBy[0] switch
                {
                    LibraryOrderBy.Name => query.OrderBy(x => x.Comic.Name),
                    LibraryOrderBy.NameDesc => query.OrderByDescending(x => x.Comic.Name),
                    LibraryOrderBy.CreationTime => query.OrderBy(x => x.CreationTime),
                    LibraryOrderBy.CreationTimeDesc => query.OrderByDescending(x => x.CreationTime),
                    _ => query.OrderBy(x => x.Comic.Name)
                };
                return search.OrderBy.Skip(1)
                    .Aggregate(ordered, (current, orderBy) => orderBy switch
                    {
                        LibraryOrderBy.Name => current.ThenBy(x => x.Comic.Name),
                        LibraryOrderBy.NameDesc => current.ThenByDescending(x => x.Comic.Name),
                        LibraryOrderBy.CreationTime => current.ThenBy(x => x.CreationTime),
                        LibraryOrderBy.CreationTimeDesc => current.ThenByDescending(x => x.CreationTime),
                        _ => current.ThenBy(x => x.Comic.Name)
                    });
            })
        };

    protected override IQueryable<LibraryEntry> ListQuery()
    {
        return Repository.GetAllByUserId(User.GetIdString());
    }

    [HttpPost("{comicId}/categories/")]
    [SwaggerOperation("Add comic in library to categories, this will return the categories that were added.")]
    public ActionResult<IEnumerable<LibraryCategoryModel>> AddToCategories(ulong comicId,
        [FromBody] ulong[] categoryIds, [FromServices] LibraryCategoryRepository categoryRepository)
    {
        ulong userId = User.GetId();
        LibraryEntry? libraryEntry = Repository.GetLibraryEntry(userId, comicId);
        if (libraryEntry == null)
        {
            return NotFound();
        }

        ulong[] notExistingCategories = categoryIds.Except(libraryEntry.Categories.Select(x => x.Id)).ToArray();
        LibraryCategory[] categories = categoryRepository.Get(userId, notExistingCategories).ToArray();
        if (categories.Length == 0)
        {
            ModelState.AddModelError("Categories", "There are no valid categories.");
            return ValidationProblem(ModelState);
        }

        libraryEntry.Categories.AddRange(categories);
        Repository.Update(libraryEntry);
        return Ok(Mapper.Map<IEnumerable<LibraryCategoryModel>>(categories));
    }

    // Remove from library category
    [HttpDelete("{comicId}/categories")]
    [SwaggerOperation("Remove comic in library from categories, this will return the categories that were removed.")]
    public ActionResult<IEnumerable<LibraryCategoryModel>> RemoveFromCategory(ulong comicId,
        [FromBody] ulong[] categoryIds, [FromServices] LibraryCategoryRepository categoryRepository)
    {
        ulong userId = User.GetId();
        LibraryEntry? libraryEntry = Repository.GetLibraryEntry(userId, comicId);
        if (libraryEntry == null)
        {
            return NotFound();
        }

        LibraryCategory[] categories = categoryRepository.Get(userId, categoryIds).ToArray();

        if (categories.Length == 0)
        {
            ModelState.AddModelError("Categories", "There are no valid categories.");
            return ValidationProblem(ModelState);
        }

        List<LibraryCategory> removedCategories =
            categories.Where(category => libraryEntry.Categories.Remove(category)).ToList();
        Repository.Update(libraryEntry);
        return Ok(Mapper.Map<IEnumerable<LibraryCategoryModel>>(removedCategories));
    }

    protected override LibraryEntry? GetEntity(ulong key)
    {
        return Repository.GetLibraryEntry(User.GetId(), key);
    }

    [HttpPost]
    [SwaggerOperation("Add comic to library (follow comic)")]
    public override ActionResult<LibraryEntryModel> Post(LibraryEntryModel input)
    {
        input.UserId = User.GetIdString();
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        LibraryEntry entity = Mapper.Map<LibraryEntry>(input);
        Logger.LogDebug("After mapped {Entity}", JsonConvert.SerializeObject(entity));
        try
        {
            Repository.Add(entity);
        }
        catch (DbUpdateException e)
        {
            Logger.LogWarning(e, "Error when adding entity");
            return Conflict();
        }
        catch (Exception e)
        {
            Logger.LogCritical(e, "Critical error when adding entity");
            return Problem();
        }

        RemoveListCache();
        if (Equals(entity.Id, default(ulong)))
        {
            return Problem("Id is null");
        }

        entity = Repository.Get(entity.Id) ?? entity;
        LibraryEntryModel? model = Mapper.Map<LibraryEntryModel>(entity);
        ModelWriteOnlyProperties.ForEach(x => x.SetValue(model, default));
        return CreatedAtAction("Get", new { comicId = entity }, model);
    }

    [HttpGet("category/{categoryId}")]
    [SwaggerOperation("List library entries by its category")]
    public ActionResult<PagedList<LibraryEntryModel>> ListByCategory([FromRoute] ulong categoryId,
        [FromQuery] LibrarySearchModel search, [FromQuery] PaginationModel pagination)
    {
        search.CategoryId = categoryId;
        return base.List(search, pagination);
    }

    [HttpGet("category/default")]
    [SwaggerOperation("List library entries with default category")]
    public ActionResult<PagedList<LibraryEntryModel>> ListByDefaultCategory([FromQuery] LibrarySearchModel search,
        [FromQuery] PaginationModel pagination)
    {
        search.HasNoCategory = true;
        return base.List(search, pagination);
    }

    [HttpGet("{comicId}")]
    [SwaggerOperation("Get library entry by comic")]
    public override ActionResult<LibraryEntryModel> Get(ulong comicId)
    {
        return base.Get(comicId);
    }

    [NonAction]
    public override ActionResult<LibraryEntryModel> Patch(ulong comicId, JsonPatchDocument<LibraryEntryModel> input)
    {
        return base.Patch(comicId, input);
    }

    [HttpDelete("{comicId}")]
    [SwaggerOperation("Delete library entry by comic (unfollow comic)")]
    public override ActionResult Delete(ulong comicId)
    {
        return base.Delete(comicId);
    }
}