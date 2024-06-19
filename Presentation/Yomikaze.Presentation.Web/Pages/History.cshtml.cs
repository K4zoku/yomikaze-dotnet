using Microsoft.AspNetCore.Mvc.RazorPages;
using Yomikaze.Presentation.Web.Pages.Shared;

namespace Yomikaze.Presentation.Web.Pages;

public class HistoryModel : PageModel
{
    public PaginationModel Pagination { get; set; } = new();

    public void OnGet(PaginationModel pagination)
    {
        Pagination = pagination;
    }
}