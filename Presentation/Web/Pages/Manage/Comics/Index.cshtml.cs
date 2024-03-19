using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Yomikaze.Presentation.Web.Pages.Shared;

namespace Yomikaze.Presentation.Web.Pages.Manage.Comics;

public class IndexModel : PageModel
{
    public PaginationModel Pagination { get; set; } = new();

    public void OnGet(PaginationModel pagination)
    {
        Pagination = pagination;
    }
}