namespace Yomikaze.Domain.Abstracts;

public interface IRepository<T, in TKey> where T : class, IEntity<TKey>
{
    IQueryable<T> Query();
    T? Get(TKey id);
    void Add(T entity);

    void Add(params T[] entities);

    void Update(T entity);
    void Delete(T entity);
}

public interface IRepository<T> : IRepository<T, ulong> where T : class, IEntity
{
}