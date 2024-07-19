using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Yomikaze.Infrastructure.Context.Generator;

public class LastModifiedGenerator : ValueGenerator<DateTimeOffset?>
{
    public override DateTimeOffset? Next(EntityEntry entry)
    {
        return entry.State == EntityState.Modified ? DateTimeOffset.UtcNow : null;
    }

    public override bool GeneratesTemporaryValues { get; } = false;
}