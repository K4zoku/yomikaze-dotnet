using Microsoft.EntityFrameworkCore;
using Yomikaze.Domain.Common;

namespace Yomikaze.Infrastructure.Data;

public abstract class BaseDao<TEntity, TId> : IBaseDao<TEntity, TId> where TEntity : BaseEntity<TId>
{
    
    protected DbContext DbContext { get; }
    protected DbSet<TEntity> DbSet => DbContext.Set<TEntity>();
    
    
    protected BaseDao(DbContext dbContext)
    {
        DbContext = dbContext;
    }
    
    public Task<TEntity?> FindByIdAsync(TId id)
    {
        var findAsync = DbSet.FindAsync(id);
        return findAsync.AsTask();
    }

    public Task AddAsync(TEntity entity)
    {
        return DbSet.AddAsync(entity).AsTask();
    }

    public Task<TEntity> UpdateAsync(TEntity entity)
    {
        return Task.FromResult(DbSet.Update(entity).Entity);
    }
    
    public void SaveChanges()
    {
        DbContext.SaveChanges();
    }

    public Task<TEntity> DeleteAsync(TEntity entity)
    {
        return Task.FromResult(DbSet.Remove(entity).Entity);
    }

    public async Task<TEntity?> DeleteByIdAsync(TId id)
    {
        var entity = await FindByIdAsync(id);
        if (entity is null) return null;
        return await DeleteAsync(entity);
    }

    public Task<bool> ExistsAsync(TId id)
    {
        return Task.FromResult(DbSet.Any(x => x.Id != null && x.Id.Equals(id)));
    }

    public Task<bool> ExistsAsync(Func<TEntity, bool> predicate)
    {
        return Task.FromResult(DbSet.Any(predicate));
    }

    public Task<long> CountAsync()
    {
        return DbSet.LongCountAsync();
    }

    public Task<IEnumerable<TEntity>> FindAllAsync()
    {
        return Task.FromResult(DbSet.AsEnumerable());
    }

    public Task<IEnumerable<TEntity>> FindAllAsync(Func<TEntity, bool> predicate)
    {
        return Task.FromResult(DbSet.Where(predicate).AsEnumerable());
    }

    public Task<IEnumerable<TEntity>> FindAllAsync(int page, int size)
    {
        return Task.FromResult(DbSet.Skip(page * size).Take(size).AsEnumerable());
    }

    public Task<IEnumerable<TEntity>> FindAllAsync(Func<TEntity, bool> predicate, int page, int size)
    {
        return Task.FromResult(DbSet.Where(predicate).Skip(page * size).Take(size).AsEnumerable());
    }
}