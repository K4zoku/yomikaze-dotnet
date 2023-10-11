namespace Yomikaze.Domain.Common;

public interface IBaseDao<TEntity, in TId> where TEntity : BaseEntity<TId>
{
    public Task<TEntity?> FindByIdAsync(TId id);

    public Task AddAsync(TEntity entity);
    
    public Task<TEntity> UpdateAsync(TEntity entity);
    
    public void SaveChanges();
    
    public Task<TEntity> DeleteAsync(TEntity entity);
    
    public Task<TEntity?> DeleteByIdAsync(TId id);
    
    public Task<bool> ExistsAsync(TId id);
    
    public Task<bool> ExistsAsync(Func<TEntity, bool> predicate);
    
    public Task<long> CountAsync();
    
    public Task<IEnumerable<TEntity>> FindAllAsync();
    
    public Task<IEnumerable<TEntity>> FindAllAsync(Func<TEntity, bool> predicate);
    
    public Task<IEnumerable<TEntity>> FindAllAsync(int page, int size);
    
    public Task<IEnumerable<TEntity>> FindAllAsync(Func<TEntity, bool> predicate, int page, int size);
    
}