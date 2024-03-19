using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Yomikaze.Presentation.Web.Pages.Manage.Comics;

public class AddChapter : PageModel
{
    
    public string ComicId { get; set; } = string.Empty;
    
    public void OnGet(string comicId)
    {
        ComicId = comicId;
    }

}