namespace Yomikaze.Domain.Models;

public class TagInputModel
{
    [Required]
    [Length(1, 50, ErrorMessage = "Genre's name must be between 1 and 100 characters")]
    public string Name { get; set; } = default!;

    public string? Description { get; set; }
}

public class TagOutputModel
{
    public ulong Id { get; set; }
    
    public string IdStr { get; set; }

    public string Name { get; set; } = default!;

    public string? Description { get; set; }
}