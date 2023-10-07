namespace Yomikaze.Domain.Common;

public class BaseEntity<TId>
{
    public TId Id { get; set; }
    public bool IsDeleted { get; set; }
}