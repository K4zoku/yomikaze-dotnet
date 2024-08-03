using Yomikaze.Domain.Entities;

namespace Yomikaze.Domain.Models;

public class TransactionModel : BaseModel
{
    #region WriteOnlyProperties

    [SwaggerSchema(WriteOnly = true)] public string? UserId { get; set; }

    #endregion

    #region CommonProperties

    public long Amount { get; set; }

    public string Description { get; set; } = default!;

    public TransactionType Type { get; set; }

    #endregion
}