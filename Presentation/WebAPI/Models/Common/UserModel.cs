using System.Text.Json.Serialization;
using Yomikaze.Domain.Database.Entities.Identity;

namespace Yomikaze.WebAPI.Models.Common;

public class UserModel
{
    public long Id { get; set; }
    public string? Fullname { get; set; } = default!;
    public string? Email { get; set; } = default!;
    public string? Username { get; set; } = default!;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Avatar { get; set; } = default!;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Banner { get; set; } = default!;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Bio { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTimeOffset? Birthday { get; set; }

    public UserModel()
    {
    }

    public UserModel(User user)
    {
        Id = user.Id;
        Fullname = user.Fullname;
        Email = user.Email;
        Username = user.UserName;
        Avatar = user.Avatar;
        Banner = user.Banner;
        Bio = user.Bio;
        Birthday = user.Birthday;
    }
}
