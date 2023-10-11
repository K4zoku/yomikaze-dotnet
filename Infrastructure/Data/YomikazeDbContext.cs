using Microsoft.EntityFrameworkCore;
using Yomikaze.Domain.Entities;

namespace Yomikaze.Infrastructure.Data;

public class YomikazeDbContext : DbContext
{
    private YomikazeDbContext(DbContextOptions<YomikazeDbContext> options) : base(options)
    {
    }
    
    public DbSet<Chapter> Chapters { get; set; } = default!;
    public DbSet<Comic> Comics { get; set; } = default!;
    public DbSet<Genre> Genres { get; set; } = default!;
    public DbSet<History> Histories { get; set; } = default!;
    public DbSet<Transaction> Transactions { get; set; } = default!;
    public DbSet<User> Users { get; set; } = default!;
    
}