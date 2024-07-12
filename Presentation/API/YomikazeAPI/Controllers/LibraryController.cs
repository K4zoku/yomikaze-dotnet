using Yomikaze.Application.Helpers.API;

namespace Yomikaze.API.Main.Controllers;

[Authorize(Roles = "Administrator,Publisher,Reader")]
[Route("[controller]")]
[ApiController]
public class LibraryController(
    LibraryRepository repository,
    IMapper mapper,
    ILogger<LibraryController> logger)
    : SearchControllerBase<LibraryEntry, LibraryEntryModel, LibraryRepository, LibrarySearchModel>(repository, mapper, logger)
{
    protected override IList<SearchFieldMutator<LibraryEntry, LibrarySearchModel>> SearchFieldMutators { get; } = new List<SearchFieldMutator<LibraryEntry, LibrarySearchModel>>
    {
        #pragma warning disable CA1862
        new(search => !string.IsNullOrWhiteSpace(search.Category), 
            (query, search) => query.Where(x => x.Categories.Any(c => c.Name == search.Category!))),
        new(search => !string.IsNullOrWhiteSpace(search.Name), 
            (query, search) => query.Where(x => x.Comic.Name.ToLower().Contains(search.Name!.ToLower()))),
        #pragma warning restore CA1862
        new(search => search.OrderBy.Length > 0, (query, search) =>
        {
            IOrderedQueryable<LibraryEntry> ordered = search.OrderBy[0] switch
            {
                LibraryOrderBy.Name => query.OrderBy(x => x.Comic.Name),
                LibraryOrderBy.NameDesc => query.OrderByDescending(x => x.Comic.Name),
                LibraryOrderBy.CreationTime => query.OrderBy(x => x.CreationTime),
                LibraryOrderBy.CreationTimeDesc => query.OrderByDescending(x => x.CreationTime),
                _ => query.OrderBy(x => x.Comic.Name),
            };
            return search.OrderBy.Skip(1)
                .Aggregate(ordered, (current, orderBy) => orderBy switch
                {
                    LibraryOrderBy.Name => current.ThenBy(x => x.Comic.Name),
                    LibraryOrderBy.NameDesc => current.ThenByDescending(x => x.Comic.Name),
                    LibraryOrderBy.CreationTime => current.ThenBy(x => x.CreationTime),
                    LibraryOrderBy.CreationTimeDesc => current.ThenByDescending(x => x.CreationTime),
                    _ => current.ThenBy(x => x.Comic.Name),
                });
        }),
    };

    protected override IQueryable<LibraryEntry> ListQuery()
    {
        return Repository.GetAllByUserId(User.GetIdString());
    }
    
    // Add to library category
    [HttpPut("{key}/categories/{categoryKey}")]
    [Authorize]
    public ActionResult AddToCategory(ulong key, ulong categoryKey, [FromServices] LibraryCategoryRepository categoryRepository)
    {
        var userId = User.GetId();
        var libraryEntry = Repository.Get(key);
        if (libraryEntry == null)
        {
            return NotFound();
        }
        var category = categoryRepository.Get(categoryKey);
        if (category == null)
        {
            ModelState.AddModelError(nameof(categoryKey), "Category not found.");
            return ValidationProblem(ModelState);
        }
        if (libraryEntry.UserId != userId)
        {
            return Forbid();
        }
        libraryEntry.Categories.Add(category);
        Repository.Update(libraryEntry);
        return NoContent();
    } 
    
    // Remove from library category
    [HttpDelete("{key}/categories/{categoryKey}")]
    [Authorize]
    public ActionResult RemoveFromCategory(ulong key, ulong categoryKey, [FromServices] LibraryCategoryRepository categoryRepository)
    {
        var userId = User.GetId();
        var libraryEntry = Repository.Get(key);
        if (libraryEntry == null)
        {
            return NotFound();
        }
        var category = categoryRepository.Get(categoryKey);
        if (category == null)
        {
            ModelState.AddModelError(nameof(categoryKey), "Category not found.");
            return ValidationProblem(ModelState);
        }
        if (libraryEntry.UserId != userId)
        {
            return Forbid();
        }
        libraryEntry.Categories.Remove(category);
        Repository.Update(libraryEntry);
        return NoContent();
    }
}