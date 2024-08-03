using Yomikaze.Domain.Entities;

namespace Yomikaze.Domain.Models;

public class ProfileModel : BaseModel
{
    public string? Avatar { get; set; }

    public string? Banner { get; set; }

    public string? Bio { get; set; }

    [Required] public string Name { get; set; } = default!;

    public DateTimeOffset? Birthday { get; set; }

    public long? Balance { get; set; }

    public string[] Roles { get; set; } = [];

    // cast from User
    public static explicit operator ProfileModel(User user)
    {
        return new ProfileModel
        {
            Id = user.Id.ToString(),
            Avatar = user.Avatar,
            Banner = user.Banner,
            Bio = user.Bio,
            Name = user.Name,
            Birthday = user.Birthday,
            Balance = user.Balance
        };
    }
}