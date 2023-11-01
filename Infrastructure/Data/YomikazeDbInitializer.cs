using Microsoft.EntityFrameworkCore;
using Yomikaze.Domain.Common;
using Yomikaze.Domain.Database.Entities;

namespace Yomikaze.Infrastructure.Data;

public sealed partial class YomikazeDbInitializer : DbInitializer<YomikazeDbContext>
{
    private bool genreSeeded = false;
    private bool comicSeeded = false;

    public YomikazeDbInitializer(YomikazeDbContext dbContext) : base(dbContext)
    {
        genreSeeded = IsTableNotEmpty<Genre>();
        comicSeeded = IsTableNotEmpty<Comic>();
    }

    protected override bool IsInitialized()
    {
        return genreSeeded && comicSeeded;
    }

    protected override void Seed()
    {
        DbContext.Genres.AddRange(DefaultData.Genres);
    }

    protected override async Task SeedAsync()
    {
        if (!genreSeeded) await DbContext.Genres.AddRangeAsync(DefaultData.Genres);
        if (!comicSeeded) await DbContext.Comics.AddRangeAsync(DefaultData.Comics);
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