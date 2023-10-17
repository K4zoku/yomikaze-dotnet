using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Yomikaze.Domain.Entities;
using Yomikaze.Domain.Entities.Identity;

namespace Yomikaze.Infrastructure.Data;

public class YomikazeDbContext : IdentityDbContext<YomikazeUser, IdentityRole<long>, long>
{

    public YomikazeDbContext() { }
    
    public YomikazeDbContext(DbContextOptions<YomikazeDbContext> options) : base(options) { }
    
    public DbSet<Chapter> Chapters { get; set; } = default!;
    public DbSet<Comic> Comics { get; set; } = default!;
    public DbSet<Genre> Genres { get; set; } = default!;
    public DbSet<HistoryRecord> Histories { get; set; } = default!;
    public DbSet<Page> Pages { get; set; } = default!;
    public DbSet<Alias> Aliases { get; set; } = default!;
    public DbSet<Artist> Artists { get; set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;
        // optionsBuilder.UseLazyLoadingProxies();
        optionsBuilder.UseSqlServer("Server=127.0.0.1;Trusted_Connection=True;TrustServerCertificate=True;Database=Yomikaze");
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(YomikazeDbContext).Assembly);
    }
}