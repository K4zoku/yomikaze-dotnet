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
    private WithdrawalRequestRepository Repository { get; } = repository;

    protected override IList<SearchFieldMutator<WithdrawalRequest, WithdrawalRequestSearchModel>> SearchFieldMutators
    {
        get;
    } =
    [
        new SearchFieldMutator<WithdrawalRequest, WithdrawalRequestSearchModel>(
            search => search.Status.HasValue,
            (query, search) => query.Where(x => x.Status == search.Status)
        ),
        new SearchFieldMutator<WithdrawalRequest, WithdrawalRequestSearchModel>(
            search => search.OrderBy == WithdrawalRequestOrderBy.CreationTime,
            (query, search) => query.OrderBy(x => x.CreationTime)
        ),
        new SearchFieldMutator<WithdrawalRequest, WithdrawalRequestSearchModel>(
            search => search.OrderBy == WithdrawalRequestOrderBy.CreationTimeDesc,
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
    public async Task<ActionResult<WithdrawalRequestModel>> Post(WithdrawalRequestModel input,
        [FromServices] UserManager<User> userManager, [FromServices] TransactionRepository transactionRepository)
    {
        User user = User.GetUser(userManager);
        input.UserId = user.Id.ToString();
        if (user.Balance >= input.Amount)
        {
            user.Balance -= input.Amount;
            await userManager.UpdateAsync(user);
            Transaction transaction = new()
            {
                UserId = user.Id, Amount = -input.Amount, Type = TransactionType.Withdrawal
            };
            transactionRepository.Add(transaction);
            return base.Post(input);
        }

        ModelState.AddModelError(nameof(input.Amount), "Insufficient balance");
        return BadRequest(ModelState);
    }

    [HttpPut]
    [Route("{id}/approve")]
    [Authorize]
    public async Task<ActionResult<WithdrawalRequestModel>> Approve(ulong id,
        [FromServices] UserManager<User> userManager)
    {
        WithdrawalRequest? request = Repository.Get(id);
        if (request is null)
        {
            return NotFound("Withdrawal request not found");
        }

        User? user = await userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null)
        {
            return NotFound("User not found");
        }

        request.Status = WithdrawalRequestStatus.Approved;
        Repository.Update(request);
        return Mapper.Map<WithdrawalRequestModel>(request);
    }

    [HttpPut]
    [Route("{id}/reject")]
    [Authorize]
    public async Task<ActionResult<WithdrawalRequestModel>> Reject(ulong id,
        [FromServices] UserManager<User> userManager, [FromServices] TransactionRepository transactionRepository)
    {
        WithdrawalRequest? request = Repository.Get(id);
        if (request is null)
        {
            return NotFound("Withdrawal request not found");
        }

        User? user = await userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null)
        {
            return NotFound("User not found");
        }

        user.Balance += request.Amount;
        await userManager.UpdateAsync(user);
        Transaction transaction = new()
        {
            UserId = user.Id,
            Amount = request.Amount,
            Type = TransactionType.WithdrawalRejected,
            Description = "Withdrawal request rejected"
        };
        transactionRepository.Add(transaction);

        request.Status = WithdrawalRequestStatus.Rejected;
        Repository.Update(request);
        return Mapper.Map<WithdrawalRequestModel>(request);
    }
}