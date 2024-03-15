using System.Text.Json.Serialization;

namespace Yomikaze.Application.Data.Models.Common;


public class ChapterInputModel
{
    public int Index { get; set; }

    public string Title { get; set; } = default!;

    public string? Description { get; set; }

    public string Pages { get; set; } = default!;

    public DateTimeOffset? Available { get; set; } = DateTimeOffset.Now;
}

public class ChapterOutputModel
{   
    public long Id { get; set; }

    public int Index { get; set; }

    public string Title { get; set; } = default!;

    public string? Description { get; set; }

    public DateTimeOffset? Available { get; set; }

    public ICollection<PageOutputModel> Pages { get; set; } = new List<PageOutputModel>();
}
