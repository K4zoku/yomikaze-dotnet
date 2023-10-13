using Microsoft.EntityFrameworkCore;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Infrastructure.Data;

public class YomikazeDbContext : DbContext
{
    public YomikazeDbContext(DbContextOptions<YomikazeDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Chapter> Chapters { get; set; } = default!;
    public DbSet<Comic> Comics { get; set; } = default!;
    public DbSet<Genre> Genres { get; set; } = default!;
    public DbSet<History> Histories { get; set; } = default!;
    public DbSet<Transaction> Transactions { get; set; } = default!;
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Page> Pages { get; set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;
        optionsBuilder.UseInMemoryDatabase("Yomikaze");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(YomikazeDbContext).Assembly);
    }
}