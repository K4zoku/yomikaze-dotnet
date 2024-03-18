namespace Yomikaze.Domain.Abstracts;

public interface IDao<TEntity, in TId> where TEntity : class, IEntity<TId>
{
    public TEntity? GetById(TId id);

    public void Add(TEntity entity);

    public void Update(TEntity entity);

    public void Delete(TEntity entity);

    public void DeleteById(TId id);

    public long Count();

    public IEnumerable<TEntity> GetAll();

    public IQueryable<TEntity> Query();

    public void Save();
}

public interface IDao<TEntity> : IDao<TEntity, string> where TEntity : class, IEntity
{
}