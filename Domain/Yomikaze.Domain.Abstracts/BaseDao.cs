using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Yomikaze.Domain.Abstracts;

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
    
    public virtual void Add(params TEntity[] entities)
    {
        DbSet.AddRange(entities);
    }

    public virtual void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }

    public virtual void Delete(TEntity entity)
    {
        DbSet.Remove(entity);
    }
    
    public virtual void Delete(params TEntity[] entity)
    {
        DbSet.RemoveRange(entity);
    }

    public virtual void DeleteById(TId id)
    {
        TEntity? entity = GetById(id);
        if (entity is not null)
        {
            Delete(entity);
        }
    }
    
    public virtual void DeleteByIds(params TId[] ids)
    {
        DbSet.Where(e => ids.Contains(e.Id)).ExecuteDeleteAsync();
    }
    
    public virtual void DeleteAll(Expression<Func<TEntity, bool>> predicate)
    {
        DbSet.Where(predicate).ExecuteDeleteAsync();
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
    
    public virtual void Detach(TEntity entity)
    {
        DbContext.Entry(entity).State = EntityState.Detached;
    }
    
    public virtual void Detach(params TEntity[] entities)
    {
        foreach (var entity in entities)
        {
            Detach(entity);
        }
    }
    
    public virtual void DetachAll()
    {
        foreach (var entity in DbSet.Local)
        {
            Detach(entity);
        }
    }
    
    public virtual void Attach(TEntity entity)
    {
        DbSet.Attach(entity);
    }
    
    public virtual void Attach(params TEntity[] entities)
    {
        DbSet.AttachRange(entities);
    }
    
    public virtual void AttachAll()
    {
        DbSet.AttachRange(DbSet.Local);
    }

    public virtual void Save()
    {
        DbContext.SaveChanges();
    }
}

public abstract class BaseDao<TEntity>(DbContext dbContext) : BaseDao<TEntity, ulong>(dbContext), IDao<TEntity>
    where TEntity : class, IEntity;