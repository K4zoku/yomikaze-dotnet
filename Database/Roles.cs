using Yomikaze.Domain.Entities.Identity;

namespace Yomikaze.Infrastructure.Database;

public partial class YomikazeDbContext
{
    private static partial class Default
    {
        public static readonly Role[] Roles =
        [
            new Role("Administrator") { Id = 1 }
            // new Role("Publisher") { Id = 2 },
            // new Role("Reader") { Id = 3 }
        ];
    }
}