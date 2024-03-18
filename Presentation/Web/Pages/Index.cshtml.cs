using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages;

public class IndexModel : PageModel
{
    public int PageNumber { get; set; } = 1;

    public void OnGet([FromQuery] int page = 1)
    {
        PageNumber = page;
    }
}