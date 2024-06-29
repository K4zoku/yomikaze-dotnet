namespace Yomikaze.Domain.Models;

public class ComicRatingModel
{
    [Range(1, 5)]
    [Required]
    public int? Rating { get; set; }
}