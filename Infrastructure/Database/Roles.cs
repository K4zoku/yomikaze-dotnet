using Yomikaze.Domain.Entities;

namespace Yomikaze.Infrastructure.Database;

public partial class YomikazeDbContext
{
    private static partial class Default
    {
        public static readonly Role[] Roles =
        [
            new Role("Administrator"),
            new Role("Publisher"),
            new Role("Reader")
        ];
    }
}