using Microsoft.AspNetCore.JsonPatch;
using Yomikaze.Application.Data.Repos;
using Yomikaze.Application.Helpers.API;

namespace Yomikaze.API.Main.Controllers;

[Authorize(Roles = "Administrator,Publisher,Reader")]
[Route("[controller]")]
[ApiController]
public class LibraryController(DbContext dbContext, IMapper mapper, IDistributedCache cache, ILogger<CrudControllerBase<LibraryEntry, LibraryEntryModel>> logger) : CrudControllerBase<LibraryEntry, LibraryEntryModel>(dbContext, mapper, new LibraryRepository(dbContext), cache, logger)
{
    private LibraryCategoryRepository LibraryCategoryRepository { get; } = new(dbContext);
    
    [HttpPost("categories")]
    public ActionResult<LibraryCategoryModel> CreateCategory(LibraryCategoryModel input)
    {
        LibraryCategory category = Mapper.Map<LibraryCategory>(input);
        category.UserId = User.GetId();
        LibraryCategoryRepository.Add(category);
        return CreatedAtAction(nameof(GetCategory), new { key = category.Id }, Mapper.Map<LibraryCategoryModel>(category));
    }
    
    [HttpGet("categories/{key}")]
    public ActionResult<LibraryCategoryModel> GetCategory(ulong key)
    {
        LibraryCategory? category = LibraryCategoryRepository.Get(key);
        if (category == null)
        {
            return NotFound();
        }
        if (category.UserId != User.GetId())
        {
            return Forbid();
        }
        return Mapper.Map<LibraryCategoryModel>(category);
    }
    
    [HttpGet("categories")]
    public ActionResult<PagedResult> ListCategories()
    {
        var query = LibraryCategoryRepository.Query().Where(x => x.UserId == User.GetId());
        return Ok(Mapper.Map<ICollection<LibraryCategoryModel>>(query.AsEnumerable()));
    }
    
    [HttpPatch("categories/{key}")]
    public ActionResult<LibraryCategoryModel> UpdateCategory(ulong key, JsonPatchDocument<LibraryCategoryModel> patchDocument)
    {
        LibraryCategory? category = LibraryCategoryRepository.Get(key);
        if (category == null)
        {
            return NotFound();
        }
        if (category.UserId != User.GetId())
        {
            return Forbid();
        }
        LibraryCategoryModel model = Mapper.Map<LibraryCategoryModel>(category);
        patchDocument.ApplyTo(model);
        Mapper.Map(model, category);
        LibraryCategoryRepository.Update(category);
        return Ok(Mapper.Map<LibraryCategoryModel>(category));
    }
    
    [HttpDelete("categories/{key}")]
    public ActionResult DeleteCategory(ulong key)
    {
        LibraryCategory? category = LibraryCategoryRepository.Get(key);
        if (category == null)
        {
            return NotFound();
        }
        if (category.UserId != User.GetId())
        {
            return Forbid();
        }
        LibraryCategoryRepository.Delete(category);
        return NoContent();
    }
}