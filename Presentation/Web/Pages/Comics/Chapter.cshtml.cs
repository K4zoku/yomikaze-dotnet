using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Yomikaze.Presentation.Web.Pages.Comics;

public class ChapterModel : PageModel
{

    public string ComicId { get; set; } = string.Empty;
    public int Index { get; set; } = 0;

    public void OnGet(string comicId, int index)
    {
        this.ComicId = comicId;
        Index = index;
    }
}