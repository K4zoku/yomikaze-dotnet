using System.ComponentModel.DataAnnotations;

namespace Yomikaze.Domain.Common;

public class BaseAuditableEntity<TId> : BaseEntity<TId>
{
    public DateTimeOffset Created { get; set; }
    
    public string? CreatedBy { get; set; }
    
    [Timestamp]
    public DateTimeOffset LastModified { get; set; }
    
    public string? LastModifiedBy { get; set; }
}