namespace Yomikaze.Domain.Models;

public class ComicTagInputModel
{
    public string GenreId { get; set; }
}

public class ComicTagOutputModel
{
    public TagOutputModel Tag { get; set; } = default!;
}