using System.ComponentModel.DataAnnotations;

namespace Yomikaze.Domain.Models;

public class GenreInputModel
{
    [Required]
    [Length(1, 50, ErrorMessage = "Genre's name must be between 1 and 100 characters")]
    public string Name { get; set; } = default!;

    public string? Description { get; set; }
}

public class GenreOutputModel
{
    public string Id { get; set; }

    public string Name { get; set; } = default!;

    public string? Description { get; set; }
}