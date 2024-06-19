using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Yomikaze.Domain.Abstracts;
using Yomikaze.Domain.Identity.Entities;

namespace Yomikaze.Infrastructure.Context.Identity;

public partial class YomikazeIdentityDbContext : IdentityDbContext<User, Role, ulong>
{
    public YomikazeIdentityDbContext()
    {
    }
    
    public YomikazeIdentityDbContext(DbContextOptions<YomikazeIdentityDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        if (optionsBuilder.IsConfigured) return;
        throw new InvalidOperationException("No database connection string provided.");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

    }
    
}