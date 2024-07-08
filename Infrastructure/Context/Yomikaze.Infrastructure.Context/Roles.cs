using Yomikaze.Domain.Entities;

namespace Yomikaze.Infrastructure.Context;

public partial class YomikazeDbContext
{
    public static partial class Default
    {
        public static readonly Role Super = new("Super") { Id = 67464207511101440L };
        public static readonly Role Administrator = new("Administrator") { Id = 67464207515295744L };
        public static readonly Role Publisher = new("Publisher") { Id = 67464207515295745L };
        public static readonly Role Reader = new("Reader") { Id = 67464207515295746L };

        public static readonly Role[] Roles =
        [
            Super,
            Administrator,
            Publisher,
            Reader
        ];

        public static readonly Role DefaulRole = Reader; // Default role for new users
    }
}