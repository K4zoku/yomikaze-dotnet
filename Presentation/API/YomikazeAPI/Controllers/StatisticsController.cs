using Yomikaze.Infrastructure.Context;

namespace Yomikaze.API.Main.Controllers;

[ApiController]
[Route("/statistics")]
[Authorize(Roles = "Super,Administrator,Publisher")]
public class StatisticsController(ILogger<StatisticsController> logger, YomikazeDbContext context) : ControllerBase
{

    [HttpGet]
    public ActionResult GetStatistics()
    {
        var comics = context.Comics.Count();
        var chapters = context.Chapters.Count();
        var users = context.Users.Count();
        var tags = context.Tags.Count();
        var tagCategories = context.TagCategories.Count();
        var transactions = context.Transactions.Count();
        var withdrawals = context.WithdrawalRequests.Count();
        var reports = context.ChapterReports.Count() + context.ComicReports.Count() + context.ProfileReports.Count() +
                      context.Set<CommentReport>().Count();
        var comments = context.ChapterComments.Count() + context.ComicComments.Count();
        
        var income = context.Transactions.Where(x => x.Type == TransactionType.PurchaseCoin).Sum(x => x.Amount);
        var outcome = context.Transactions.Where(x => x.Type == TransactionType.Withdrawal).Sum(x => x.Amount);
        
        return Ok(new
        {
            comics,
            chapters,
            users,
            tags,
            tagCategories,
            transactions,
            withdrawals,
            reports,
            comments,
            income,
            outcome
        });
    }

    [HttpGet("comic/chart")]
    public ActionResult GetComicCharts()
    {
        var comics = context.Comics
            .GroupBy(x => x.CreationTime)
            .Select(x => new {x = x.Key, y = x.Count()})
            .OrderBy(x => x.x)
            .ToList();
        return Ok(comics); 
    }
    
    [HttpGet("financial/chart")]
    public ActionResult GetFinancialCharts()
    {
        var income = context.Transactions
            .Where(x => x.Type == TransactionType.PurchaseCoin)
            .GroupBy(x => x.CreationTime)
            .Select(x => new {x = x.Key, y = x.Sum(y => y.Amount)})
            .OrderBy(x => x.x)
            .ToList();
        var outcome = context.Transactions
            .Where(x => x.Type == TransactionType.Withdrawal)
            .GroupBy(x => x.CreationTime)
            .Select(x => new {x = x.Key, y = x.Sum(y => y.Amount)})
            .OrderBy(x => x.x)
            .ToList();
        return Ok(new {income, outcome});
    }
    
}