namespace Yomikaze.Domain.Models;

public class CoinPricingModel : BaseModel
{
    public long Amount { get; set; }

    public double Price { get; set; }

    public string Currency { get; set; } = default!;

    public double Discount { get; set; }
}