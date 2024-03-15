

namespace Yomikaze.Domain.Common;

public abstract class BaseRepo<T, TKey> : IRepo<T, TKey> where T : class, IEntity<TKey>
{
    protected BaseRepo(BaseDao<T, TKey> dao)
    {
        Dao = dao;
    }

    protected BaseDao<T, TKey> Dao { get; }

    public virtual T? Get(TKey id) => Dao.GetById(id);

    public virtual void Add(T entity)
    {
        Dao.Add(entity);
        Dao.Save();
    }

    public virtual void Update(T entity)
    {
        Dao.Update(entity);
        Dao.Save();
    }

    public virtual void Delete(T entity)
    {
        Dao.Delete(entity);
        Dao.Save();
    }

    public virtual IQueryable<T> Query()
    {
        return Dao.Query();
    }
}
public abstract class BaseRepo<T> : BaseRepo<T, long>, IRepo<T> where T : class, IEntity
{
    protected BaseRepo(BaseDao<T> dao) : base(dao)
    {
    }
}