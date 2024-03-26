namespace Yomikaze.Domain.Abstracts;

public abstract class BaseRepository<T, TKey> : IRepository<T, TKey> where T : class, IEntity<TKey>
{
    protected BaseRepository(BaseDao<T, TKey> dao)
    {
        Dao = dao;
    }

    protected BaseDao<T, TKey> Dao { get; }

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
        return Dao.Query();
    }
}

public abstract class BaseRepository<T> : BaseRepository<T, string>, IRepository<T> where T : class, IEntity
{
    protected BaseRepository(BaseDao<T> dao) : base(dao)
    {
    }
}