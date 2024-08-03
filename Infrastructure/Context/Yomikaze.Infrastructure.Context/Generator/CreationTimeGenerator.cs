using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Yomikaze.Infrastructure.Context.Generator;

public class CreationTimeGenerator : ValueGenerator<DateTimeOffset>
{
    public override bool GeneratesTemporaryValues { get; } = false;

    public override DateTimeOffset Next(EntityEntry entry)
    {
        return DateTimeOffset.UtcNow;
    }
}