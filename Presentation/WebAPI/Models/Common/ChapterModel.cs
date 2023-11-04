using System.Text.Json.Serialization;
using Yomikaze.Domain.Database.Entities;
using Yomikaze.WebAPI.Helpers;

namespace Yomikaze.WebAPI.Models.Common;

public class ChapterModel
{
    public long Id { get; set; }
    public int Index { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public DateTimeOffset? Available { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual ICollection<PageModel> Pages { get; set; } = default!;

    public static explicit operator ChapterModel(Chapter chapter)
    {
        return new()
        {
            Id = chapter.Id,
            Index = chapter.Index,
            Title = chapter.Title,
            Description = chapter.Description,
            Available = chapter.Available,
            Pages = chapter.Pages.Select(p => p.ToModel()).ToList()
        };
    }
}
