using Yomikaze.Domain.Constants;

namespace Yomikaze.Domain.Common;

public class SnowflakeGenerator
{
    private long _template;
    private uint _sequence;
    
    public SnowflakeGenerator(long epoch, uint dataCenterId, uint workerId)
    {
        _template = (epoch << 41) | (dataCenterId << 22) | (workerId << 17);
    }
    
    public SnowflakeGenerator(uint dataCenterId, uint workerId) : this(SnowflakeConstants.Epoch, dataCenterId, workerId)
    {
    }
    
    public Snowflake Next()
    {
        var snowflake = _template | _sequence;
        _sequence = (_sequence + 1) & 0xFFF;
        return new Snowflake(snowflake);
    }
}