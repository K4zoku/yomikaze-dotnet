using Microsoft.EntityFrameworkCore;
using Yomikaze.Domain.Common;
using Yomikaze.Domain.Database.Entities;

namespace Yomikaze.Infrastructure.Data;

public sealed partial class YomikazeDbInitializer : DbInitializer<YomikazeDbContext>
{
    private bool genreSeeded = false;
    private bool comicSeeded = false;
    private bool isInitialized = false;

    public YomikazeDbInitializer(YomikazeDbContext dbContext) : base(dbContext)
    {
    }

    protected override bool IsInitialized()
    {
        if (!isInitialized)
        {
            genreSeeded = IsTableNotEmpty<Genre>();
            comicSeeded = IsTableNotEmpty<Comic>();
            isInitialized = true;
        }
        return genreSeeded && comicSeeded;
    }

    protected override void Seed()
    {
        DbContext.Genres.AddRange(DefaultData.Genres);
        DbContext.Comics.AddRange(DefaultData.Comics);
        DbContext.Chapters.AddRange(DefaultData.Chapters);
        DbContext.Pages.AddRange(DefaultData.Pages);
    }

    protected override async Task SeedAsync()
    {
        await DbContext.Genres.AddRangeAsync(DefaultData.Genres);
        await DbContext.Comics.AddRangeAsync(DefaultData.Comics);
        await DbContext.Chapters.AddRangeAsync(DefaultData.Chapters);
        await DbContext.Pages.AddRangeAsync(DefaultData.Pages);
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