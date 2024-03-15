using Microsoft.EntityFrameworkCore;

namespace Abstracts;

public abstract class BaseDao<TEntity, TId>(DbContext dbContext) : IDao<TEntity, TId>
    where TEntity : class, IEntity<TId>
{
    protected DbContext DbContext { get; } = dbContext;
    protected DbSet<TEntity> DbSet => DbContext.Set<TEntity>();


    public virtual TEntity? GetById(TId id)
    {
        return Query().FirstOrDefault(e => e.Id != null && e.Id.Equals(id));
    }

    public virtual void Add(TEntity entity)
    {
        DbSet.Add(entity);
    }

    public virtual void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }

    public virtual void Delete(TEntity entity)
    {
        DbSet.Remove(entity);
    }

    public virtual void DeleteById(TId id)
    {
        TEntity? entity = GetById(id);
        if (entity is not null)
        {
            Delete(entity);
        }
    }

    public virtual long Count()
    {
        return DbSet.LongCount();
    }

    public virtual IEnumerable<TEntity> GetAll()
    {
        return Query().AsEnumerable();
    }

    public virtual IQueryable<TEntity> Query()
    {
        return DbSet.AsQueryable();
    }

    public virtual void Save()
    {
        DbContext.SaveChanges();
    }
}

public abstract class BaseDao<TEntity>(DbContext dbContext) : BaseDao<TEntity, long>(dbContext), IDao<TEntity>
    where TEntity : class, IEntity;