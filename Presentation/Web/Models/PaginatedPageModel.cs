using Microsoft.AspNetCore.Mvc.RazorPages;
using Yomikaze.Presentation.Web.Pages.Shared;

namespace Yomikaze.Presentation.Web.Models;

public class PaginatedPageModel : PageModel
{
    public PaginationModel Pagination { get; set; } = new();
    
    public virtual void OnGet(PaginationModel pagination)
    {
        Pagination = pagination;
    }
}