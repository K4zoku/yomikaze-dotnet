namespace Yomikaze.Application.Data.Models.Common;

public class LibraryEntryInputModel
{
    public long UserId { get; set; }

    public long ComicId { get; set; }

    public DateTimeOffset DateAdded { get; set; } = DateTimeOffset.Now;
}

public class LibraryEntryOutputModel
{
    public long Id { get; set; }

    public ComicOutputModel Comic { get; set; } = default!;

    public UserOutputModel User { get; set; } = default!;

    public DateTimeOffset DateAdded { get; set; }
}