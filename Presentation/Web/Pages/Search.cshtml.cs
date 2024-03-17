using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages;

public class SearchModel : PageModel
{
    
    public int PageNumber { get; set; } = 1;
    public string Query { get; set; } = string.Empty;

    public void OnGet([FromQuery] int page = 1, string? query = null)
    {
        PageNumber = page;
        Query = query ?? string.Empty;
    }
}