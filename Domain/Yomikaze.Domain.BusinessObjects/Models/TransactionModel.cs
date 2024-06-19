namespace Yomikaze.Domain.Models;

public class TransactionModel : BaseModel
{
    #region ReadWriteProperties

    [Required]
    public long Amount { get; set; }
    
    [Required]
    public string Description { get; set; } = default!;

    #endregion

    #region WriteOnlyProperties

    [SwaggerSchema(WriteOnly = true)]
    public string? UserId { get; set; }

    #endregion
}