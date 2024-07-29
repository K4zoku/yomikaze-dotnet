using Microsoft.AspNetCore.Identity;
using Yomikaze.Application.Helpers.API;
using Yomikaze.Domain.Models.Search;

namespace Yomikaze.API.Main.Controllers;

[Route("/withdrawal")]
[Authorize]
[ApiController]
public class WithdrawalController(
    WithdrawalRequestRepository repository,
    IMapper mapper,
    ILogger<WithdrawalController> logger)
    : SearchControllerBase<WithdrawalRequest, WithdrawalRequestModel, WithdrawalRequestRepository,
        WithdrawalRequestSearchModel>(repository, mapper, logger)
{
    protected override IList<SearchFieldMutator<WithdrawalRequest, WithdrawalRequestSearchModel>> SearchFieldMutators
    { get; } =
    [
        new SearchFieldMutator<WithdrawalRequest, WithdrawalRequestSearchModel>(
            (search) => search.Status.HasValue,
            (query, search) => query.Where(x => x.Status == search.Status)
        ),
        new SearchFieldMutator<WithdrawalRequest, WithdrawalRequestSearchModel>(
            (search) => search.OrderBy == WithdrawalRequestOrderBy.CreationTime,
            (query, search) => query.OrderBy(x => x.CreationTime)
        ),
        new SearchFieldMutator<WithdrawalRequest, WithdrawalRequestSearchModel>(
            (search) => search.OrderBy == WithdrawalRequestOrderBy.CreationTimeDesc,
            (query, search) => query.OrderByDescending(x => x.CreationTime)
        )
    ];

    [NonAction]
    public override ActionResult<WithdrawalRequestModel> Post(WithdrawalRequestModel input)
    {
        return base.Post(input);
    }

    [Authorize(Roles = "Administrator,Publisher")]
    [HttpPost]
    public ActionResult<WithdrawalRequestModel> Post(WithdrawalRequestModel input, [FromServices] UserManager<User> userManager)
    {
        User user = User.GetUser(userManager);
        input.UserId = user.Id.ToString();
        if (user.Balance >= input.Amount)
        {
            return base.Post(input);
        }

        ModelState.AddModelError(nameof(input.Amount), "Insufficient balance");
        return BadRequest(ModelState);

    }
}