using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Comics;

public class Details : PageModel
{
    public string Id { get; set; } = string.Empty;

    public void OnGet(string id)
    {
        Id = id;
    }
}