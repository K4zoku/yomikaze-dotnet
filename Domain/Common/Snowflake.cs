using Yomikaze.Domain.Constants;

namespace Yomikaze.Domain.Common;

[Serializable]
public partial class Snowflake 
{
    
    private static readonly Random Rng = new();
    public long Value { get; }
    
    // 1 bit for sign
    // 41 bits for timestamp in milliseconds
    // 5 bits for data center id
    // 5 bits for worker id
    // and 12 bits for sequence
    public DateTimeOffset Timestamp => DateTimeOffset.FromUnixTimeMilliseconds((Value >> 22) + SnowflakeConstants.Epoch);
    public uint DataCenterId => (uint)(Value >> 22) & 0x1F;
    public uint WorkerId => (uint)(Value >> 17) & 0x1F;
    public uint Sequence => (uint)Value & 0xFFF;
    
    public Snowflake(long value)
    {
        Value = value;
    }
    
    public static Snowflake Random()
    {
        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - SnowflakeConstants.Epoch;
        var dataCenterId = (uint)Rng.Next(0, 31);
        var workerId = (uint)Rng.Next(0, 31);
        var sequence = (uint)Rng.Next(0, 4095);
        var snowflake = (timestamp << 41) | (dataCenterId << 22) | (workerId << 17) | sequence;
        return new Snowflake(snowflake);
    }


}