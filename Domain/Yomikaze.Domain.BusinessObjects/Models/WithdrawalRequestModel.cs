using Yomikaze.Domain.Entities;

namespace Yomikaze.Domain.Models;

public class WithdrawalRequestModel : BaseModel
{
    #region ReadOnlyProperties

    [SwaggerSchema(ReadOnly = true)] public ProfileModel Profile { get; set; } = default!;

    #endregion

    #region WriteOnlyProperties

    [SwaggerSchema(WriteOnly = true)] public string UserId { get; set; } = default!;

    #endregion

    #region ReadWriteProperties

    [Required] public long Amount { get; set; }

    public WithdrawalRequestStatus Status { get; set; }

    public string? RejectionReason { get; set; }

    #endregion
}