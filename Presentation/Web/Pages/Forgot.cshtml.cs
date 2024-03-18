using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages;

public class Forgot : PageModel
{
    public string Email { get; set; }
    
    public void OnGet([FromQuery] string email)
    {
        Email = email;
    }
}