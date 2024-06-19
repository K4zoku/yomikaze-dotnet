using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Yomikaze.Presentation.Web.Pages;

public class Forgot : PageModel
{
    public string Email { get; set; } = default!;

    public void OnGet([FromQuery] string email)
    {
        Email = email;
    }
}