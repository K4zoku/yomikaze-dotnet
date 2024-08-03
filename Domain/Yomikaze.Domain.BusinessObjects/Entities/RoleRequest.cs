namespace Yomikaze.Domain.Entities;

public class RoleRequest : BaseEntity
{
    public ulong UserId { get; set; }

    public User User { get; set; } = default!;

    public ulong RoleId { get; set; }

    public Role Role { get; set; } = default!;

    public RoleRequestStatus Status { get; set; } = RoleRequestStatus.Pending;

    [StringLength(512)] public string Reason { get; set; } = default!;
}

public enum RoleRequestStatus
{
    Pending,
    Approved,
    Rejected
}