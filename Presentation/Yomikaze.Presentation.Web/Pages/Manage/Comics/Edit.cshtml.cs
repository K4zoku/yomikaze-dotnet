using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Yomikaze.Presentation.Web.Pages.Manage.Comics;

public class EditModel : PageModel
{
    public string Id { get; set; } = default!;

    public void OnGet(string id)
    {
        Id = id;
    }
}