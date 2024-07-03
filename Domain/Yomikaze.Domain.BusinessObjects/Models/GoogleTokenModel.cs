namespace Yomikaze.Domain.Models;

public class GoogleTokenModel
{
    [Required]
    public string? Token { get; set; }
}