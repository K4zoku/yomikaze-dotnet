using Microsoft.EntityFrameworkCore;
using Yomikaze.Domain.Common;
using Yomikaze.Domain.Database.Entities;

namespace Yomikaze.Infrastructure.Data;

public sealed partial class YomikazeDbInitializer : DbInitializer<YomikazeDbContext>
{
    public YomikazeDbInitializer(YomikazeDbContext dbContext) : base(dbContext)
    {
    }
    
    protected override bool IsInitialized()
    {
        return IsTableNotEmpty<Genre>();
    }
    
    protected override void Seed()
    {
        DbContext.Genres.AddRange(DefaultData.Genres);   
    }
    
    protected override async Task SeedAsync()
    {
        await DbContext.Genres.AddRangeAsync(DefaultData.Genres);
    }

    protected override void Migrate()
    {
        DbContext.Database.Migrate();
    }
    
    protected override async Task MigrateAsync()
    {
        await DbContext.Database.MigrateAsync();
    }
}