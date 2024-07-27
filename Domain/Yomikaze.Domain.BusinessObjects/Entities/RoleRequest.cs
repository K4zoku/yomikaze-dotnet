namespace Yomikaze.Domain.Entities;

public class RoleRequest : BaseEntity
{
    public ulong UserId { get; set; }
    
    public ulong RoleId { get; set; }
    
    public bool IsApproved { get; set; }
    
    [StringLength(512)]
    public string Reason { get; set; } = default!;
}