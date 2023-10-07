namespace Yomikaze.Domain.Constants;

public class SnowflakeConstants
{
    public static readonly DateTimeOffset DateTime = new(2023, 10, 1, 0, 0, 0, new TimeSpan(7,0,0));
    public static readonly long Epoch = DateTime.ToUnixTimeSeconds();
}