using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yomikaze.Domain.Entities;
using Yomikaze.Infrastructure.Context.Generator;

namespace Yomikaze.Infrastructure.Context.Configuration;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");
        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .HasValueGenerator<SnowflakeIdGenerator>();
    }
}