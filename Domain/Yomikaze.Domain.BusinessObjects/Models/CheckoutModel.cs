namespace Yomikaze.Domain.Models;

public class CheckoutModel
{
    [Required]
    [SwaggerSchema("The ID of the pricing to checkout.")]
    public string PriceId { get; set; } = default!;

    [Required]
    [Url]
    [SwaggerSchema(
        "The URL to redirect to after the checkout is completed. The actual session ID will be appended to the URL as `session_id` query parameter.")]
    public string ReturnUrl { get; set; } = default!;
}

public class CheckoutResultModel
{
    public string SessionId { get; set; } = default!;
    public string ClientSecret { get; set; } = default!;
}