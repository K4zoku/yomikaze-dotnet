using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Comic;

public class Details : PageModel
{
    public long Id { get; set; }
    
    public void OnGet(long id)
    {
        Id = id;
    }
}