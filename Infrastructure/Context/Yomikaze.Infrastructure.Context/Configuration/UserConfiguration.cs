using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Infrastructure.Context.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder
            .HasMany(e => e.Roles)
            .WithMany()
            .UsingEntity<IdentityUserRole<ulong>>();
        builder
            .Navigation(e => e.Roles)
            .AutoInclude();
    }
}