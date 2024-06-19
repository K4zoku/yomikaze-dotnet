using Microsoft.AspNetCore.Mvc.RazorPages;
using Yomikaze.Presentation.Web.Pages.Shared;

namespace Yomikaze.Presentation.Web.Pages;

public class SearchModel : PageModel
{
    public string? Query { get; set; }
    public string? Genres { get; set; }
    public string? Authors { get; set; }
    
    public PaginationModel Pagination { get; set; } = new();

    public void OnGet(PaginationModel pagination, string? query = null, string? genres = null, string? authors = null)
    {
        Pagination = pagination;
        Query = query;
        Genres = genres;
        Authors = authors;
    }
}