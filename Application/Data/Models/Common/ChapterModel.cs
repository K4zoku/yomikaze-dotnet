using System.Text.Json.Serialization;

namespace Yomikaze.Application.Data.Models.Common;

public class ChapterModel
{
    public long Id { get; set; }
    public int Index { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public DateTimeOffset? Available { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual ICollection<PageModel> Pages { get; set; } = default!;
}