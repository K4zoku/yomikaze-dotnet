using Microsoft.EntityFrameworkCore;

namespace Yomikaze.Domain.Common;

public abstract class BaseDao<TEntity, TId> : IDao<TEntity, TId> where TEntity : class, IEntity<TId>
{

    protected DbContext DbContext { get; }
    protected DbSet<TEntity> DbSet => DbContext.Set<TEntity>();

    protected BaseDao(DbContext dbContext)
    {
        DbContext = dbContext;
    }

    public virtual async Task<TEntity?> GetAsync(TId id)
    {
        return await DbSet.FirstOrDefaultAsync(entity => entity.Id != null && entity.Id.Equals(id));
    }

    public virtual async Task AddAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
    }

    public virtual Task<TEntity> UpdateAsync(TEntity entity)
    {
        return Task.FromResult(DbSet.Update(entity).Entity);
    }

    public virtual void SaveChanges()
    {
        DbContext.SaveChanges();
    }

    public virtual Task<TEntity> DeleteAsync(TEntity entity)
    {
        return Task.FromResult(DbSet.Remove(entity).Entity);
    }

    public virtual async Task<TEntity?> DeleteByIdAsync(TId id)
    {
        var entity = await GetAsync(id);
        if (entity is null) return null;
        return await DeleteAsync(entity);
    }

    public virtual Task<bool> ExistsAsync(TId id)
    {
        return Task.FromResult(DbSet.Any(x => x.Id != null && x.Id.Equals(id)));
    }

    public virtual Task<bool> ExistsAsync(Func<TEntity, bool> predicate)
    {
        return Task.FromResult(DbSet.Any(predicate));
    }

    public virtual Task<long> CountAsync()
    {
        return DbSet.LongCountAsync();
    }

    public virtual Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return Task.FromResult(DbSet.AsEnumerable());
    }

    public virtual Task<IEnumerable<TEntity>> FindAsync(Func<TEntity, bool> predicate)
    {
        return Task.FromResult(DbSet.Where(predicate).AsEnumerable());
    }

    public virtual Task<IEnumerable<TEntity>> FindAsync(int page, int size)
    {
        return Task.FromResult(DbSet.Skip(page * size).Take(size).AsEnumerable());
    }

    public virtual Task<IEnumerable<TEntity>> FindAsync(Func<TEntity, bool> predicate, int page, int size)
    {
        return Task.FromResult(DbSet.Where(predicate).Skip(page * size).Take(size).AsEnumerable());
    }

    public virtual Task<IQueryable<TEntity>> QueryAsync()
    {
        return Task.FromResult(DbSet.AsQueryable());
    }
}

public abstract class BaseDao<TEntity> : BaseDao<TEntity, long> where TEntity : class, IEntity
{
    protected BaseDao(DbContext dbContext) : base(dbContext)
    {
    }
}