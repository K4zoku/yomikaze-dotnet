using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Infrastructure.Context.Configuration;

public class CoinPricingConfiguration : BaseEntityConfiguration<CoinPricing>
{
    public override void Configure(EntityTypeBuilder<CoinPricing> builder)
    {
        base.Configure(builder);
        builder
            .Property(e => e.Currency)
            .HasConversion(
                v=> v.ToString(),
                v=> Enum.Parse<Currency>(v));
    }
}