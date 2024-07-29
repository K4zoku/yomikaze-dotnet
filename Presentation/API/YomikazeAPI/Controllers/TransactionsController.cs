using Microsoft.AspNetCore.JsonPatch;
using Yomikaze.Application.Helpers.API;

namespace Yomikaze.API.Main.Controllers;

[Route("/transactions")]
[ApiController]
[Authorize]
public class TransactionsController(
    TransactionRepository repository,
    IMapper mapper,
    ILogger<SearchControllerBase<Transaction, TransactionModel, TransactionRepository, TransactionSearchModel>> logger)
    : SearchControllerBase<Transaction, TransactionModel, TransactionRepository, TransactionSearchModel>(repository,
        mapper,
        logger)
{
    protected override IList<SearchFieldMutator<Transaction, TransactionSearchModel>> SearchFieldMutators { get; } =
    [
        new SearchFieldMutator<Transaction, TransactionSearchModel>(search => !string.IsNullOrWhiteSpace(search.UserId),
            (query, search) => query.Where(transaction => transaction.UserId.ToString() == search.UserId)),
        new SearchFieldMutator<Transaction, TransactionSearchModel>(search => search.FromAmount.HasValue,
            (query, search) => query.Where(transaction => transaction.Amount >= search.FromAmount)),
        new SearchFieldMutator<Transaction, TransactionSearchModel>(search => search.ToAmount.HasValue,
            (query, search) => query.Where(transaction => transaction.Amount <= search.ToAmount)),
        new SearchFieldMutator<Transaction, TransactionSearchModel>(search => search.FromDate != default,
            (query, search) => query.Where(transaction => transaction.CreationTime >= search.FromDate)),
        new SearchFieldMutator<Transaction, TransactionSearchModel>(search => search.ToDate != default,
            (query, search) => query.Where(transaction => transaction.CreationTime <= search.ToDate)),
        new SearchFieldMutator<Transaction, TransactionSearchModel>(
            search => !string.IsNullOrWhiteSpace(search.Description),
            (query, search) => query.Where(transaction =>
                transaction.Description.ToLower().Contains(search.Description!.ToLower()))),
        new SearchFieldMutator<Transaction, TransactionSearchModel>(search => search.OrderBy.HasValue,
            (query, search) => search.OrderBy switch
            {
                TransactionOrderBy.Amount => query.OrderBy(transaction => transaction.Amount),
                TransactionOrderBy.AmountDesc => query.OrderByDescending(transaction => transaction.Amount),
                TransactionOrderBy.Date => query.OrderBy(transaction => transaction.CreationTime),
                TransactionOrderBy.DateDesc => query.OrderByDescending(transaction => transaction.CreationTime),
                _ => query
            })
    ];

    [Authorize]
    [HttpGet]
    public override ActionResult<PagedList<TransactionModel>> List(TransactionSearchModel search,
        PaginationModel pagination)
    {
        search.UserId = User.GetIdString();
        return base.List(search, pagination);
    }

    [NonAction]
    public override ActionResult<TransactionModel> Get(ulong key)
    {
        return base.Get(key);
    }

    [NonAction]
    public override ActionResult<TransactionModel> Post(TransactionModel input)
    {
        return base.Post(input);
    }

    [NonAction]
    public override ActionResult<TransactionModel> Patch(ulong key, JsonPatchDocument<TransactionModel> input)
    {
        return base.Patch(key, input);
    }

    [NonAction]
    public override ActionResult Delete(ulong key)
    {
        return base.Delete(key);
    }
}