using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Manage.Comics;

public class EditModel : PageModel
{
    public long Id { get; set; }

    public void OnGet([FromRoute] long id)
    {
        Id = id;
    }
}