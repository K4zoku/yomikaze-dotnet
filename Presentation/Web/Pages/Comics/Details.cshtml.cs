using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Comics;

public class Details : PageModel
{
    public long Id { get; set; }

    public void OnGet(long id)
    {
        Id = id;
    }
}