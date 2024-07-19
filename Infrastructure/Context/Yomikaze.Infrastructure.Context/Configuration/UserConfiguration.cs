using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yomikaze.Domain.Entities;
using Yomikaze.Infrastructure.Context.Generator;

namespace Yomikaze.Infrastructure.Context.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .HasValueGenerator<SnowflakeIdGenerator>();
        builder
            .HasMany(e => e.Roles)
            .WithMany()
            .UsingEntity<IdentityUserRole<ulong>>();
        builder
            .Navigation(e => e.Roles)
            .AutoInclude();
    }
}