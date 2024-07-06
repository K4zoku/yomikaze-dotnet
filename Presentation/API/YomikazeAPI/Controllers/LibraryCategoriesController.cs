using Yomikaze.Application.Helpers.API;

namespace Yomikaze.API.Main.Controllers;

[Authorize(Roles = "Administrator,Publisher,Reader")]
[Route("library/categories")]
[ApiController]
public class LibraryCategoriesController(
    LibraryCategoryRepository repository,
    IMapper mapper,
    ILogger<CrudControllerBase<LibraryCategory, LibraryCategoryModel, LibraryCategoryRepository>> logger)
    : CrudControllerBase<LibraryCategory, LibraryCategoryModel, LibraryCategoryRepository>(repository, mapper, logger)
{
    protected override IQueryable<LibraryCategory> ListQuery()
    {
        return Repository.GetAllByUserId(User.GetIdString());
    }

    public override ActionResult<LibraryCategoryModel> Post(LibraryCategoryModel input)
    {
        input.UserId = User.GetIdString();
        return base.Post(input);
    }
}