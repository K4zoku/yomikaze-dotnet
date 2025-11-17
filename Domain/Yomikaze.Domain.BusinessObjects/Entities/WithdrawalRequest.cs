namespace Yomikaze.Domain.Entities;

[PublicAPI]
public class WithdrawalRequest : BaseEntity
{
    #region Navigation Properties

    public User User { get; set; } = null!;

    #endregion

    #region Properties

    public ulong UserId { get; set; }

    public WithdrawalRequestStatus Status { get; set; }

    [StringLength(1024)] public string PaymentInformation { get; set; } = default!;

    public long Amount { get; set; }

    #endregion
}

public enum WithdrawalRequestStatus
{
    Pending = 0,
    Approved = 1,
    Rejected = 2
}