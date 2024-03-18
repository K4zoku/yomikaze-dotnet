using System.ComponentModel.DataAnnotations;

namespace Yomikaze.Domain.Models;

public class ChapterInputModel
{
    public string ComicId { get; set; }

    public int Index { get; set; }

    [Required]
    [Length(0, 50, ErrorMessage = "Tittle must from 0 to 50 characters")]
    public string Title { get; set; } = default!;

    [Length(0, 250, ErrorMessage = "Description must from 0 to 250 characters")]
    public string? Description { get; set; }

    public ICollection<PageInputModel> Pages { get; set; } = new List<PageInputModel>();

    public DateTimeOffset? Available { get; set; } = DateTimeOffset.Now;
}

public class ChapterOutputModel
{
    public string Id { get; set; }

    public int Index { get; set; }

    public string Title { get; set; } = default!;

    public string? Description { get; set; }

    public DateTimeOffset? Available { get; set; }

    public ICollection<PageOutputModel>? Pages { get; set; } = new List<PageOutputModel>();
}