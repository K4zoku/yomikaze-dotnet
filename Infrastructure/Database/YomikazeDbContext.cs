using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Infrastructure.Database;

public partial class YomikazeDbContext : DbContext
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
        builder.Entity<Genre>().HasData(Default.Genres);
        builder.ApplyConfigurationsFromAssembly(typeof(YomikazeDbContext).Assembly);
    }
}