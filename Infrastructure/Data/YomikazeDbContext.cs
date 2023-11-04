using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Yomikaze.Domain.Database.Entities;
using Yomikaze.Domain.Database.Entities.Identity;

namespace Yomikaze.Infrastructure.Data;

public class YomikazeDbContext : IdentityDbContext<User, IdentityRole<long>, long>
{

    public YomikazeDbContext() { }

    public YomikazeDbContext(DbContextOptions<YomikazeDbContext> options) : base(options) { }

    public virtual DbSet<Chapter> Chapters { get; set; } = default!;
    public virtual DbSet<Comic> Comics { get; set; } = default!;
    public virtual DbSet<Comment> Comments { get; set; } = default!;
    public virtual DbSet<Genre> Genres { get; set; } = default!;
    public virtual DbSet<HistoryRecord> Histories { get; set; } = default!;
    public virtual DbSet<Notification> Notifications { get; set; } = default!;
    public virtual DbSet<Page> Pages { get; set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;
        optionsBuilder.UseSqlServer("Server=.;Trusted_Connection=True;TrustServerCertificate=True;Database=Yomikaze");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<User>().ToTable("Users");
        builder.Entity<IdentityRole<long>>().ToTable("Roles");
        builder.Entity<IdentityUserLogin<long>>().ToTable("UserLogins");
        builder.Entity<IdentityUserClaim<long>>().ToTable("UserClaims");
        builder.Entity<IdentityUserToken<long>>().ToTable("UserTokens");
        builder.Entity<IdentityUserRole<long>>().ToTable("UserRoles");
        builder.Entity<IdentityRoleClaim<long>>().ToTable("RoleClaims");
        builder.ApplyConfigurationsFromAssembly(typeof(YomikazeDbContext).Assembly);
    }
}