using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Manage.Comics;

public class EditModel : PageModel
{
    public string Id { get; set; } = string.Empty;

    public void OnGet([FromRoute] string id)
    {
        Id = id;
    }
}