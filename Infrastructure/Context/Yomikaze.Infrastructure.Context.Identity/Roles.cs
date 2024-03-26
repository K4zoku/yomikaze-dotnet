using Yomikaze.Domain.Identity.Entities;

namespace Yomikaze.Infrastructure.Context.Identity;

public partial class YomikazeIdentityDbContext
{
    private static partial class Default
    {
        public static readonly Role[] Roles =
        [
            new Role("Administrator"),
            new Role("Publisher"),
            new Role("Reader")
        ];
        
        public const string DefaultRoleName = "Reader"; // Default role for new users
    }
}