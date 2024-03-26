using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Yomikaze.Presentation.Web.Pages.Manage.Comics;

public class EditModel : PageModel
{
    public string Id { get; set; }
    public void OnGet(string id)
    {
        Id = id;
    }
}