using Microsoft.EntityFrameworkCore;
using Yomikaze.Domain.Entities;
using Yomikaze.Domain.Entities.Weak;

namespace Yomikaze.Infrastructure.Context;

public partial class YomikazeDbContext : DbContext
{
    public YomikazeDbContext() { }

    public YomikazeDbContext(DbContextOptions<YomikazeDbContext> options) : base(options) { }

    public DbSet<Author> Authors { get; init; } = default!;
    public DbSet<Chapter> Chapters { get; init; } = default!;
    public DbSet<Comic> Comics { get; init; } = default!;
    public DbSet<Comment> Comments { get; init; } = default!;
    public DbSet<Genre> Genres { get; init; } = default!;
    public DbSet<HistoryRecord> Histories { get; init; } = default!;
    public DbSet<Notification> Notifications { get; init; } = default!;
    public DbSet<Page> Pages { get; init; } = default!;
    public DbSet<LibraryEntry> LibraryEntries { get; init; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        if (optionsBuilder.IsConfigured) return;
        throw new InvalidOperationException("No database connection string provided.");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Genre>().HasData(Default.Genres);
        modelBuilder.Entity<Comic>()
            .HasMany(e => e.Genres)
            .WithMany(e => e.Comics)
            .UsingEntity<ComicGenre>();
        modelBuilder.Entity<Comic>()
            .HasMany(e => e.Authors)
            .WithMany(e => e.Comics)
            .UsingEntity<ComicAuthor>();
    }
}
