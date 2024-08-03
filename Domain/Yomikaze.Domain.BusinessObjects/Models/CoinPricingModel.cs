using Yomikaze.Domain.Entities;

namespace Yomikaze.Domain.Models;

public class CoinPricingModel : BaseModel
{
    [Required] public long? Amount { get; set; }

    [Required] public double? Price { get; set; }

    public Currency Currency { get; set; } = Currency.USD;

    [SwaggerIgnore]
    [SwaggerSchema(ReadOnly = true)]
    [WriteOnly]
    public string? StripePriceId { get; set; }
}