using Yomikaze.Domain.Database.Entities;

namespace Yomikaze.WebAPI.Models.Common;

public class GenreModel
{
    public long Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }

    public static explicit operator GenreModel(Genre genre)
    {
        return new()
        {
            Id = genre.Id,
            Name = genre.Name,
            Description = genre.Description
        };
    }
}
