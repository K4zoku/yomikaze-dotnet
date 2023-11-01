using Microsoft.EntityFrameworkCore;

namespace Yomikaze.Domain.Common;

public abstract class DbInitializer<TDbContext> where TDbContext : DbContext
{
    protected TDbContext DbContext { get; }

    protected DbInitializer(TDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public void Initialize()
    {
        Migrate();
        if (IsInitialized()) return;
        Seed();
        DbContext.SaveChanges();

    }

    public async Task InitializeAsync()
    {
        await MigrateAsync();
        if (await IsInitializedAsync()) return;
        await DbContext.Database.BeginTransactionAsync();
        await SeedAsync();
        await DbContext.Database.CommitTransactionAsync();
        await DbContext.SaveChangesAsync();
    }

    protected virtual bool IsInitialized()
    {
        return false;
    }

    protected virtual Task<bool> IsInitializedAsync()
    {
        return Task.FromResult(IsInitialized());
    }

    protected virtual void Seed()
    {

    }

    protected virtual Task SeedAsync()
    {
        return new Task(Seed);
    }

    protected virtual void Migrate()
    {

    }

    protected virtual Task MigrateAsync()
    {
        return new Task(Migrate);
    }

    protected bool IsTableNotEmpty<T>() where T : class
    {
        return DbContext.Set<T>().Any();
    }

    protected bool IsTableEmpty<T>() where T : class
    {
        return !IsTableNotEmpty<T>();
    }
}

public abstract class DbInitializer : DbInitializer<DbContext>
{
    protected DbInitializer(DbContext dbContext) : base(dbContext)
    {
    }
}