namespace Yomikaze.Application.Data.Models.Request;
public class ChapterRequestModel
{
    public int Index { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }

    public string Pages { get; set; } = default!;
}
