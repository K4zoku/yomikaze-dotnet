using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yomikaze.Domain.Entities;
using Yomikaze.Domain.Entities.Weak;

namespace Yomikaze.Infrastructure.Context.Configuration;

public class ComicConfiguration : BaseEntityConfiguration<Comic>
{
    public override void Configure(EntityTypeBuilder<Comic> builder)
    {
        base.Configure(builder);
        builder
            .HasMany(e => e.Tags)
            .WithMany(e => e.Comics)
            .UsingEntity<ComicTag>();
        builder
            .HasMany(e => e.Chapters)
            .WithOne(e => e.Comic);
        builder
            .HasMany(e=> e.Ratings)
            .WithOne(e => e.Comic);
        builder
            .HasMany(e => e.Follows)
            .WithOne(e => e.Comic);
        builder
            .HasMany(e => e.Comments)
            .WithOne(e => e.Comic);
        builder
            .Navigation(e => e.Tags)
            .AutoInclude();
        builder
            .Navigation(e => e.Views)
            .EnableLazyLoading();         
        builder
            .Navigation(e => e.Publisher)
            .AutoInclude();
    }
}