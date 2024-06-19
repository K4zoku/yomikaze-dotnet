using Yomikaze.Domain.Identity.Entities;

namespace Yomikaze.Infrastructure.Context;

public partial class YomikazeDbContext
{
    private static partial class Default
    {
        public static readonly Role Administrator = new("Administrator");
        public static readonly Role Publisher = new("Publisher");
        public static readonly Role Reader = new("Reader");

        public static readonly Role[] Roles =
        [
            Administrator,
            Publisher,
            Reader
        ];

        public static readonly Role DefaulRole = Reader; // Default role for new users
    }
}