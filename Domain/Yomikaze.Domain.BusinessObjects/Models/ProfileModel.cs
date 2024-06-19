namespace Yomikaze.Domain.Models;

public class ProfileModel : BaseModel
{
    public string? Avatar { get; set; }

    public string? Banner { get; set; }

    public string? Bio { get; set; }

    [Required] public string Name { get; set; } = default!;

    public DateTimeOffset? Birthday { get; set; }

    public long? Balance { get; set; }
}