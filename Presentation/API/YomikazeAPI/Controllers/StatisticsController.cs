using System.Collections;
using System.Runtime.Serialization;
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
            .Select(x => new { x = x.Key, y = x.Count() })
            .OrderBy(x => x.x)
            .AsEnumerable()
            .GroupBy(x => x.x?.ToString("yyyy-MM-dd"))
            .Select(x => new { x = x.Key, y = x.Sum(y => y.y) })
            .OrderBy(x => x.x)
            .ToList();
        // accumulate
        var dataset = new
        {
            Data = comics.Aggregate(new List<int>(), (acc, x) =>
            {
                if (acc.Count == 0)
                {
                    acc.Add(x.y);
                }
                else
                {
                    acc.Add(acc[^1] + x.y);
                }

                return acc;
            }),
        };
        // add date between with count = date before
        var format = new DateTimeFormat("yyyy-MM-dd");
        var start = DateTime.Parse(comics[0].x!, format.FormatProvider);
        var end = DateTime.Parse(comics[^1].x!, format.FormatProvider);
        var date = start;
        var index = 0;
        var labels = new List<string>();
        var data = new List<int>();
        while (date < end)
        {
            labels.Add(date.ToString("yyyy-MM-dd"));
            data.Add(dataset.Data[index]);
            date = date.AddDays(1);
            var next = DateTime.Parse(comics[index].x!, format.FormatProvider);
            Console.Out.WriteLine("date: {0}, next {1}", date, next);           
            if (date >= next)
            {
                index++;
            }
        }   
        var chart = new { Labels = labels, Datasets = new ArrayList { new { Data = data, } } };
        return Ok(chart);
    }

    [HttpGet("financial/chart")]
    public ActionResult GetFinancialCharts()
    {
        var income = context.Transactions
            .Where(x => x.Type == TransactionType.PurchaseCoin)
            .GroupBy(x => x.CreationTime)
            .Select(x => new { x = x.Key, y = x.Sum(y => y.Amount) })
            .OrderBy(x => x.x)
            .ToList();
        var outcome = context.Transactions
            .Where(x => x.Type == TransactionType.Withdrawal)
            .GroupBy(x => x.CreationTime)
            .Select(x => new { x = x.Key, y = x.Sum(y => y.Amount) })
            .OrderBy(x => x.x)
            .ToList();
        return Ok(new { income, outcome });
    }
}