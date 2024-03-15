namespace Yomikaze.Domain.Common;

public interface IRepo<T, TKey> where T : class, IEntity<TKey>
{
    IQueryable<T> Query();
    T? Get(TKey id);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}

public interface IRepo<T> : IRepo<T, long> where T : class, IEntity
{
}