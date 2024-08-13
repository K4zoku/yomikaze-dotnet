using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Yomikaze.Domain.Abstracts;
using Yomikaze.Domain.Entities;
using Yomikaze.Domain.Entities.Weak;
using Yomikaze.Domain.Models;

namespace Yomikaze.Infrastructure.Context;

public partial class YomikazeDbContext : IdentityDbContext<User, Role, ulong>
{
    public YomikazeDbContext()
    {
    }

    public YomikazeDbContext(DbContextOptions<YomikazeDbContext> options) : base(options)
    {
    }

    public DbSet<Chapter> Chapters { get; init; } = default!;
    public DbSet<CoinPricing> CoinPricings { get; init; } = default!;
    public DbSet<Comic> Comics { get; init; } = default!;

    public DbSet<ComicView> ComicViews { get; init; } = default!;
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
    public DbSet<ChapterReportReason> ChapterReportReasons { get; init; } = default!;
    public DbSet<ComicReportReason> ComicReportReasons { get; init; } = default!;
    public DbSet<ProfileReportReason> ProfileReportReasons { get; init; } = default!;

    public DbSet<CommentReportReason> CommentReportReasons { get; init; } = default!;
    public DbSet<TranslationReportReason> TranslationReportReasons { get; init; } = default!;
    public DbSet<Tag> Tags { get; init; } = default!;
    public DbSet<TagCategory> TagCategories { get; init; } = default!;
    public DbSet<Transaction> Transactions { get; init; } = default!;
    public DbSet<Translation> Translations { get; init; } = default!;
    public DbSet<UnlockedChapter> UnlockedChapters { get; init; } = default!;
    public DbSet<WithdrawalRequest> WithdrawalRequests { get; init; } = default!;

    public DbSet<RoleRequest> RoleRequests { get; init; } = default!;

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
    
    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry is not { State: EntityState.Modified })
            {
                continue;
            }
            if (entry is not { Entity: BaseEntity baseEntity })
            {
                continue;
            }
            
            baseEntity.LastModified = DateTimeOffset.UtcNow;
            entry.Property("LastModified").IsModified = true;
        }
        return base.SaveChanges();
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

        builder.Entity<ComicViewsResult>().ToView("comic_views");
        builder.Entity<ComicsViewResult>().ToView("comics_views");

        builder
            .HasDbFunction(GetType().GetMethod(nameof(GetComicViewsResult),
                [typeof(ulong), typeof(DateTimeOffset), typeof(DateTimeOffset)])!)
            .HasName("get_comic_views")
            .IsBuiltIn(false);

        builder
            .HasDbFunction(GetType().GetMethod(nameof(GetComicsViewsResult),
                [typeof(DateTimeOffset), typeof(DateTimeOffset)])!)
            .HasName("get_comics_views")
            .IsBuiltIn(false);
    }

    public IQueryable<ComicViewsResult> GetComicViewsResult(ulong id, DateTimeOffset startDate, DateTimeOffset endDate)
    {
        return FromExpression(() => GetComicViewsResult(id, startDate.ToUniversalTime(), endDate.ToUniversalTime()));
    }

    public IQueryable<ComicsViewResult> GetComicsViewsResult(DateTimeOffset startDate, DateTimeOffset endDate)
    {
        return FromExpression(() => GetComicsViewsResult(startDate.ToUniversalTime(), endDate.ToUniversalTime()));
    }

    public IQueryable<Comic> GetRandomComic()
    {
        return Comics.FromSqlRaw("SELECT * FROM get_random_comic");
    }
}

[Keyless]
public record ComicViewsResult(ulong Views);

[Keyless]
public record ComicsViewResult(ulong Id, ulong Views);