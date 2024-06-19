using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Yomikaze.Presentation.Web.Pages.Comics;

public class ChapterModel : PageModel
{
    public string ComicId { get; set; } = string.Empty;
    public int Index { get; set; }

    public void OnGet(string comicId, int index)
    {
        ComicId = comicId;
        Index = index;
    }
}