using Yomikaze.Domain.Common;

namespace Yomikaze.Domain.Entities;

public class User : BaseEntity<long>
{
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public bool IsBanned { get; set; }
    public bool IsEmailConfirmed { get; set; }
    public bool IsAdmin { get; set; }
    
    public string Name { get; set; } = default!;
    public string? Bio { get; set; }
    public string? Avatar { get; set; }
    public string? Banner { get; set; }
    public decimal Balance { get; set; }
    
    public IList<Comic> Library { get; private set; } = new List<Comic>();
}