using Yomikaze.Domain.Common;

namespace Yomikaze.Domain.Entities;

public class History : BaseAuditableEntity<long>
{
    public Chapter Chapter { get; set; } = default!;
    public User User { get; set; } = default!;
    
    public new string CreatedBy => User.Username;
    public new string LastModifiedBy => User.Username;
}