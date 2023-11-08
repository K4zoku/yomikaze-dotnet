namespace Yomikaze.Application.Data.Models.Request;
public class ComicRequestModel
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? Cover { get; set; }
    public string? Banner { get; set; }
    public DateTimeOffset? Published { get; set; }
    public DateTimeOffset? Ended { get; set; }
    public string? Aliases { get; set; }
    public string? Authors { get; set; }

    public string Genres { get; set; } = default!;
}
