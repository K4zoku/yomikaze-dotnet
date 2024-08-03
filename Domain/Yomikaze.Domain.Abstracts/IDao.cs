using System.Linq.Expressions;

namespace Yomikaze.Domain.Abstracts;

public interface IDao<TEntity, in TId> where TEntity : class, IEntity<TId>
{
    public TEntity? GetById(TId id);

    public void Add(TEntity entity);

    public void Add(params TEntity[] entities);

    public void Update(TEntity entity);

    public void Delete(TEntity entity);

    public void Delete(params TEntity[] entity);

    public void DeleteById(TId id);

    public void DeleteByIds(params TId[] ids);

    public void DeleteAll(Expression<Func<TEntity, bool>> predicate);

    public long Count();

    public IEnumerable<TEntity> GetAll();

    public IQueryable<TEntity> Query();

    public void Save();
}

public interface IDao<TEntity> : IDao<TEntity, ulong> where TEntity : class, IEntity
{
}