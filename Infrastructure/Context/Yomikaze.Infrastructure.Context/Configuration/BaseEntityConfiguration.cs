using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yomikaze.Domain.Abstracts;
using Yomikaze.Infrastructure.Context.Generator;

namespace Yomikaze.Infrastructure.Context.Configuration;

public abstract class BaseEntityConfiguration<TEntity, TKey> : IEntityTypeConfiguration<TEntity>
    where TEntity : BaseEntity<TKey>, IEntity<TKey>
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        
        builder
            .Property(e => e.CreationTime)
            .IsRequired()
            .ValueGeneratedOnAdd()
            .HasValueGenerator<CreationTimeGenerator>();
        builder
            .Property(e => e.LastModified)
            .ValueGeneratedOnUpdate()
            .HasValueGenerator<LastModifiedGenerator>();
    }
}

public abstract class BaseEntityConfiguration<TEntity> : BaseEntityConfiguration<TEntity, ulong>
    where TEntity : BaseEntity, IEntity
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);
        builder
            .Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .HasValueGenerator<SnowflakeIdGenerator>();
    }
}

public class BaseConfiguration<TEntity> : BaseEntityConfiguration<TEntity>
    where TEntity : BaseEntity, IEntity
{
}