using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Yomikaze.Domain.Abstracts;
using Yomikaze.Domain.Entities;
using Yomikaze.Domain.Entities.Weak;

namespace Yomikaze.Infrastructure.Context;

public partial class YomikazeDbContext : DbContext
{

    public YomikazeDbContext()
    {
    }

    public YomikazeDbContext(DbContextOptions<YomikazeDbContext> options) : base(options)
    {
    }
    
    public DbSet<Chapter> Chapters { get; init; } = default!;
    public DbSet<Comic> Comics { get; init; } = default!;
    public DbSet<Comment> Comments { get; init; } = default!;
    public DbSet<Tag> Genres { get; init; } = default!;
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

    private void OnSaveChanges()
    {
        IEnumerable<EntityEntry> entries = ChangeTracker.Entries()
            .Where(e => e is { State: EntityState.Added or EntityState.Modified, Entity: BaseEntity });
        
        foreach (EntityEntry entry in entries)
        {
            BaseEntity entity = (BaseEntity)entry.Entity;
            switch (entry)
            {
                case { State: EntityState.Added } when entity.CreationTime == default:
                    entity.CreationTime = DateTime.UtcNow;
                    break;
                case { State: EntityState.Modified } when entity.LastModified == null:
                    entity.LastModified = DateTime.UtcNow;
                    break;
            }
        }
    }

    public override int SaveChanges()
    {
        OnSaveChanges();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnSaveChanges();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Tag>().HasData(Default.Genres);
        modelBuilder.Entity<Comic>()
            .HasMany(e => e.Genres)
            .WithMany(e => e.Comics)
            .UsingEntity<ComicTags>();

    }
}
