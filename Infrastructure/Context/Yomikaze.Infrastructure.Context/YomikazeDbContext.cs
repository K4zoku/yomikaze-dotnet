using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Yomikaze.Domain.Abstracts;
using Yomikaze.Domain.Entities;
using Yomikaze.Domain.Entities.Weak;
using Yomikaze.Infrastructure.Context.Configuration;
using Yomikaze.Infrastructure.Context.Generator;

namespace Yomikaze.Infrastructure.Context;

public partial class YomikazeDbContext : IdentityDbContext<User, Role, ulong>
{
    public YomikazeDbContext()
    {
        SaveChangesEvent += OnSaveChanges;
    }

    public YomikazeDbContext(DbContextOptions<YomikazeDbContext> options) : base(options)
    {
        SaveChangesEvent += OnSaveChanges;
    }

    public DbSet<Chapter> Chapters { get; init; } = default!;
    public DbSet<CoinPricing> CoinPricings { get; init; } = default!;
    public DbSet<Comic> Comics { get; init; } = default!;
    public DbSet<ComicRating> ComicRatings { get; init; } = default!;
    public DbSet<ChapterComment> ChapterComments { get; init; } = default!;
    public DbSet<ComicComment> ComicComments { get; init; } = default!;
    public DbSet<ProfileComment> ProfileComments { get; init; } = default!;
    public DbSet<CommentReaction> CommentReactions { get; init; } = default!;
    public DbSet<HistoryRecord> HistoryRecords { get; init; } = default!;
    public DbSet<LibraryCategory> LibraryCategories { get; init; } = default!;
    public DbSet<LibraryEntry> LibraryEntries { get; init; } = default!;
    public DbSet<Notification> Notifications { get; init; } = default!;
    public DbSet<Page> Pages { get; init; } = default!;
    public DbSet<ChapterReport> ChapterReports { get; init; } = default!;
    public DbSet<ComicReport> ComicReports { get; init; } = default!;
    public DbSet<ProfileReport> ProfileReports { get; init; } = default!;
    public DbSet<TranslationReport> TranslationReports { get; init; } = default!;
    public DbSet<ReportCategory> ReportCategories { get; init; } = default!;
    public DbSet<Tag> Tags { get; init; } = default!;
    public DbSet<TagCategory> TagCategories { get; init; } = default!;
    public DbSet<Transaction> Transactions { get; init; } = default!;
    public DbSet<Translation> Translations { get; init; } = default!;
    public DbSet<UnlockedChapter> UnlockedChapters { get; init; } = default!;
    public DbSet<WithdrawalRequest> WithdrawalRequests { get; init; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseProjectables();
        if (optionsBuilder.IsConfigured)
        {
            return;
        }

        throw new InvalidOperationException("No database connection string provided.");
    }

    private event Action? SaveChangesEvent;

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        SaveChangesEvent?.Invoke();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {               
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        
        builder.Entity<IdentityUserToken<ulong>>().ToTable("user_tokens");
        builder.Entity<IdentityUserLogin<ulong>>().ToTable("user_logins");
        builder.Entity<IdentityUserClaim<ulong>>().ToTable("user_claims");
        builder.Entity<Role>().ToTable("roles");
        builder.Entity<IdentityUserRole<ulong>>().ToTable("user_roles");
        builder.Entity<IdentityRoleClaim<ulong>>().ToTable("role_claims");
        
        builder.Entity<TagCategory>().HasData(Default.TagCategories);
        builder.Entity<Tag>().HasData(Default.Tags);
        builder.Entity<Role>().HasData(Default.Roles);
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
                case { State: EntityState.Modified }:
                    entity.LastModified = DateTime.UtcNow;
                    break;
            }
        }
    }
}