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
        int comics = context.Comics.Count();
        int chapters = context.Chapters.Count();
        int users = context.Users.Count();
        int tags = context.Tags.Count();
        int tagCategories = context.TagCategories.Count();
        int transactions = context.Transactions.Count();
        int withdrawals = context.WithdrawalRequests.Count();
        int reports = context.ChapterReports.Count() + context.ComicReports.Count() + context.ProfileReports.Count() +
                      context.Set<CommentReport>().Count();
        int comments = context.ChapterComments.Count() + context.ComicComments.Count();
        int roleRequests = context.RoleRequests.Count();

        long income = context.Transactions.Where(x => x.Type == TransactionType.PurchaseCoin).Sum(x => x.Amount);
        long outcome = context.Transactions.Where(x => x.Type == TransactionType.Withdrawal).Sum(x => x.Amount);

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
            roleRequests,
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
            })
        };
        // add date between with count = date before
        DateTimeFormat format = new DateTimeFormat("yyyy-MM-dd");
        DateTime start = DateTime.Parse(comics[0].x!, format.FormatProvider);
        DateTime end = DateTime.Parse(comics[^1].x!, format.FormatProvider);
        DateTime date = start;
        int index = 0;
        List<string> labels = new List<string>();
        List<int> data = new List<int>();
        while (date < end)
        {
            labels.Add(date.ToString("yyyy-MM-dd"));
            data.Add(dataset.Data[index]);
            date = date.AddDays(1);
            DateTime next = DateTime.Parse(comics[index].x!, format.FormatProvider);
            if (date >= next)
            {
                index++;
            }
        }

        var chart = new { Labels = labels, Datasets = new ArrayList { new { Data = data } } };
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

    [HttpGet("report")]
    public ActionResult GetReports()
    {
        int chapterReports = context.ChapterReports.Count();
        int comicReports = context.ComicReports.Count();
        int profileReports = context.ProfileReports.Count();
        int commentReports = context.Set<CommentReport>().Count();
        return Ok(new { chapterReports, comicReports, profileReports, commentReports });
    }
}