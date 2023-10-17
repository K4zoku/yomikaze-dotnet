namespace Yomikaze.Domain.Common;

public interface IDao<TEntity, in TId> where TEntity : IEntity<TId>
{
    public Task<TEntity?> GetAsync(TId id);

    public Task AddAsync(TEntity entity);
    
    public Task<TEntity> UpdateAsync(TEntity entity);
    
    public void SaveChanges();
    
    public Task<TEntity> DeleteAsync(TEntity entity);
    
    public Task<TEntity?> DeleteByIdAsync(TId id);
    
    public Task<bool> ExistsAsync(TId id);
    
    public Task<bool> ExistsAsync(Func<TEntity, bool> predicate);
    
    public Task<long> CountAsync();
    
    public Task<IEnumerable<TEntity>> GetAllAsync();
    
    public Task<IEnumerable<TEntity>> FindAsync(Func<TEntity, bool> predicate);
    
    public Task<IEnumerable<TEntity>> FindAsync(int page, int size);
    
    public Task<IEnumerable<TEntity>> FindAsync(Func<TEntity, bool> predicate, int page, int size);
    
}

public interface IDao<TEntity> : IDao<TEntity, long> where TEntity : IEntity
{
    
}