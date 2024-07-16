namespace Yomikaze.Domain.Models;

public class PaymentSheetModel
{
    [Required]
    [SwaggerSchema("The ID of the pricing to checkout.")]
    public string PriceId { get; set; } = default!;
}

public class PaymentSheetResultModel
{
    public string ClientSecret { get; set; } = default!;
    public string PublishableKey { get; set; } = default!;
    
    public string EphemeralKey { get; set; } = default!;

    public string Customer { get; set; } = default!;
}