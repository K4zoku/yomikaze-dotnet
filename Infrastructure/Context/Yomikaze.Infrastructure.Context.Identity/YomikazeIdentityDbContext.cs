using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Yomikaze.Domain.Identity.Entities;

namespace Yomikaze.Infrastructure.Context.Identity;

public partial class YomikazeIdentityDbContext : IdentityDbContext<User, Role, string>
{
    public YomikazeIdentityDbContext()
    {
    }
    
    public YomikazeIdentityDbContext(DbContextOptions<YomikazeIdentityDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        if (optionsBuilder.IsConfigured) return;
        throw new InvalidOperationException("No database connection string provided.");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(YomikazeIdentityDbContext).Assembly);
        builder.Entity<User>().ToTable("users");
        builder.Entity<Role>().ToTable("roles").HasData(Default.Roles);
        builder.Entity<IdentityUserLogin<string>>().ToTable("user_logins");
        builder.Entity<IdentityUserClaim<string>>().ToTable("user_claims");
        builder.Entity<IdentityUserToken<string>>().ToTable("user_tokens");
        builder.Entity<IdentityUserRole<string>>().ToTable("user_roles");
        builder.Entity<IdentityRoleClaim<string>>().ToTable("role_claims");
    }
    
}