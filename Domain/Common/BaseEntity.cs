namespace Yomikaze.Domain.Common;

public abstract class BaseEntity<TId> : IEntity<TId>
{
    public TId Id { get; } = default!;
}

public abstract class BaseEntity : BaseEntity<long>
{
}