using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Yomikaze.Domain.Abstracts;

namespace Yomikaze.Infrastructure.Context.Generator;

public class SnowflakeIdGenerator(int workerId = 0) : ValueGenerator<ulong>
{
    
    public override ulong Next(EntityEntry entry)
    {
        if (entry.Entity is BaseEntity entity)
        {
            return SnowflakeGenerator.Generate(entity.WorkerId);
        }
        return SnowflakeGenerator.Generate(workerId);
    }

    public override bool GeneratesTemporaryValues { get; } = false;
}