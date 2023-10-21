using System.ComponentModel.DataAnnotations;

namespace Yomikaze.WebAPI.Models;

public abstract class UsernamePasswordModel
{
    [Required]
    public string Username { get; set; } = default!;

    [Required]
    public string Password { get; set; } = default!;
}