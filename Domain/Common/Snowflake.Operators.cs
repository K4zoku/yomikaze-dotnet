namespace Yomikaze.Domain.Common;

public partial class Snowflake
{
    
    public static implicit operator long(Snowflake snowflake)
    {
        return snowflake.Value;
    }
    
    public static implicit operator Snowflake(long value)
    {
        return new Snowflake(value);
    }
    
    public static bool operator ==(Snowflake left, Snowflake right)
    {
        return left.Value == right.Value;
    }
    
    public static bool operator !=(Snowflake left, Snowflake right)
    {
        return left.Value != right.Value;
    }
    
    public static bool operator <(Snowflake left, Snowflake right)
    {
        return left.Value < right.Value;
    }
    
    public static bool operator >(Snowflake left, Snowflake right)
    {
        return left.Value > right.Value;
    }
    
    public static bool operator <=(Snowflake left, Snowflake right)
    {
        return left.Value <= right.Value;
    }
    
    public static bool operator >=(Snowflake left, Snowflake right)
    {
        return left.Value >= right.Value;
    }
    
    public override bool Equals(object? obj)
    {
        return obj is Snowflake snowflake && Value == snowflake.Value;
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(Value);
    }
    
    public override string ToString()
    {
        return Value.ToString();
    }

}