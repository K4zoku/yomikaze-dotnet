namespace Yomikaze.Domain.Models;

public class TransactionSearchModel : BaseModel
{
    [SwaggerIgnore] public string? UserId { get; set; }

    public long? FromAmount { get; set; }

    public long? ToAmount { get; set; }

    public DateTimeOffset FromDate { get; set; }

    public DateTimeOffset ToDate { get; set; }

    public string? Description { get; set; } = default!;

    public TransactionOrderBy? OrderBy { get; set; }
}

public enum TransactionOrderBy
{
    Amount,
    AmountDesc,
    Date,
    DateDesc
}