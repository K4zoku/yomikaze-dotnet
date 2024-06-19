namespace Yomikaze.Domain.Entities;

public class CoinPricing : BaseEntity
{
    public long Amount { get; set; }

    public double Price { get; set; }

    public string Currency { get; set; } = default!;

    public double Discount { get; set; }
}