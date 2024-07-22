using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Infrastructure.Context.Configuration;

public class ChapterConfiguration : BaseEntityConfiguration<Chapter>
{
    public override void Configure(EntityTypeBuilder<Chapter> builder)
    {
        base.Configure(builder);

        builder
            .Navigation(x => x.Unlocked)
            .EnableLazyLoading();
    }
}