namespace Yomikaze.Domain.Models;

public class ComicGenreInputModel
{
    public string GenreId { get; set; }
}

public class ComicGenreOutputModel
{
    public GenreOutputModel Genre { get; set; } = default!;
}