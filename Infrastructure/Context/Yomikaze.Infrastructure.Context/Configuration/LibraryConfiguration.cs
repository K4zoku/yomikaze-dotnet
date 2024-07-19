using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yomikaze.Domain.Entities;
using Yomikaze.Domain.Entities.Weak;

namespace Yomikaze.Infrastructure.Context.Configuration;

public class LibraryConfiguration : BaseEntityConfiguration<LibraryEntry>
{
    public override void Configure(EntityTypeBuilder<LibraryEntry> builder)
    {
        base.Configure(builder);
        
        builder
            .HasMany(e => e.Categories)
            .WithMany(e => e.Entries)
            .UsingEntity<LibraryEntryCategory>();
        builder
            .Navigation(e => e.Comic)
            .AutoInclude();
        builder
            .Navigation(e => e.Categories)
            .AutoInclude();
    }
}