using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using SnowflakeID;
using Yomikaze.Domain.Abstracts;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Infrastructure.Context.Generator;

public class SnowflakeIdGenerator : ValueGenerator<ulong>
{
    private static readonly DateTime Epoch = new(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);


    private static readonly Dictionary<int, SnowflakeIDGenerator> Generators = new();

    private static readonly object Lock = new();

    public override bool GeneratesTemporaryValues => false;

    public static ulong Generate(int workerId = 0)
    {
        workerId &= 0x1F; // 5 bits
        if (Generators.TryGetValue(workerId, out SnowflakeIDGenerator? generator))
        {
            return generator.GetCode();
        }

        lock (Lock)
        {
            generator = new SnowflakeIDGenerator(workerId, Epoch);
            Generators.Add(workerId, generator);

            return generator.GetCode();
        }
    }


    public override ulong Next(EntityEntry entry)
    {
        return entry.Entity switch
        {
            BaseEntity entity => Generate(entity.WorkerId),
            User user => Generate(user.WorkerId),
            Role role => Generate(role.WorkerId),
            _ => Generate()
        };
    }
}