using SnowflakeID;

namespace Yomikaze.Domain.Abstracts;

public static class SnowflakeGenerator
{
    private static readonly DateTime Epoch = new(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);


    private static readonly Dictionary<int, SnowflakeIDGenerator> Generators = new();

    private static readonly object Lock = new();

    public static string Generate(int workerId = 0)
    {
        workerId &= 0x1F; // 5 bits
        lock (Lock)
        {
            if (Generators.TryGetValue(workerId, out SnowflakeIDGenerator? value))
            {
                return value.GetCodeString();
            }

            value = new SnowflakeIDGenerator(workerId, Epoch);
            Generators.Add(workerId, value);

            return value.GetCodeString();
        }
    }
}