using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yomikaze.Domain.Entities;
using Yomikaze.Infrastructure.Context.Generator;

namespace Yomikaze.Infrastructure.Context.Configuration;

public class ComicViewConfiguration : IEntityTypeConfiguration<ComicView>
{
    public void Configure(EntityTypeBuilder<ComicView> builder)
    {
        // need to manually create hypertable timescaledb on postgres with timescaledb extension
        // or manually edit the migration file to create hypertable
        builder
            .HasIndex(x => x.CreationTime);
        builder.Property(x => x.CreationTime)
            .IsRequired()
            .ValueGeneratedOnAdd()
            .HasValueGenerator<CreationTimeGenerator>();
        builder.HasOne<Comic>()
            .WithMany()
            .HasForeignKey(x => x.ComicId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}