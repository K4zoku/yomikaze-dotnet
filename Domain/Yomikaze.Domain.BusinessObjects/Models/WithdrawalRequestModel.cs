using Yomikaze.Domain.Entities;

namespace Yomikaze.Domain.Models;

public class WithdrawalRequestModel : BaseModel
{
    #region ReadOnlyProperties

    [SwaggerSchema(ReadOnly = true)] public ProfileModel Profile { get; set; } = default!;

    [SwaggerSchema(ReadOnly = true)]
    public WithdrawalRequestStatus Status { get; set; } = WithdrawalRequestStatus.Pending;
    
    #endregion

    #region WriteOnlyProperties

    [SwaggerIgnore]
    [SwaggerSchema(ReadOnly = true)]
    public string UserId { get; set; } = default!;

    #endregion

    #region CommonProperties

    [Required] public long Amount { get; set; }

    #endregion
}