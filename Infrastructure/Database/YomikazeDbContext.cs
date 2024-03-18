using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Infrastructure.Database;

public partial class YomikazeDbContext : IdentityDbContext<User, Role, ulong>
{
    public YomikazeDbContext() { }

    public YomikazeDbContext(DbContextOptions<YomikazeDbContext> options) : base(options) { }

    public virtual DbSet<Chapter> Chapters { get; init; } = default!;
    public virtual DbSet<Comic> Comics { get; init; } = default!;
    public virtual DbSet<Comment> Comments { get; init; } = default!;
    public virtual DbSet<Genre> Genres { get; init; } = default!;
    public virtual DbSet<HistoryRecord> Histories { get; init; } = default!;
    public virtual DbSet<Notification> Notifications { get; init; } = default!;
    public virtual DbSet<Page> Pages { get; init; } = default!;
    public virtual DbSet<LibraryEntry> LibraryEntries { get; init; } = default!;
    public virtual DbSet<ComicGenre> ComicGenres { get; init; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
        {
            return;
        }

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false, true)
            .Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            server => server.EnableRetryOnFailure());
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<User>().ToTable("Users");
        builder.Entity<Role>().ToTable("Roles").HasData(Default.Roles);
        builder.Entity<IdentityUserLogin<ulong>>().ToTable("UserLogins");
        builder.Entity<IdentityUserClaim<ulong>>().ToTable("UserClaims");
        builder.Entity<IdentityUserToken<ulong>>().ToTable("UserTokens");
        builder.Entity<IdentityUserRole<ulong>>().ToTable("UserRoles");
        builder.Entity<IdentityRoleClaim<ulong>>().ToTable("RoleClaims");
        builder.Entity<Genre>().HasData(Default.Genres);
        builder.ApplyConfigurationsFromAssembly(typeof(YomikazeDbContext).Assembly);
    }
}