namespace Yomikaze.Domain.Abstracts;

public abstract class BaseRepository<T, TKey>(BaseDao<T, TKey> dao) : IRepository<T, TKey>
    where T : class, IEntity<TKey>
{
    protected BaseDao<T, TKey> Dao { get; } = dao;

    public virtual T? Get(TKey id)
    {
        return Dao.GetById(id);
    }

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
        // default order by creation time desc (latest first) which is using snowflake id
        return Dao.Query().OrderByDescending(entity => entity.Id);
    }
}

public abstract class BaseRepository<T>(BaseDao<T, ulong> dao) : BaseRepository<T, ulong>(dao), IRepository<T>
    where T : class, IEntity;