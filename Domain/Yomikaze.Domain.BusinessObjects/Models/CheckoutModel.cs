namespace Yomikaze.Domain.Models;

public class CheckoutModel
{
    [Required]
    [SwaggerSchema("The ID of the pricing to checkout.")]
    public string PriceId { get; set; }
    
    [Required]
    [Url]
    [SwaggerSchema("The URL to redirect to after the checkout is completed. The actual session ID will be appended to the URL as `session_id` query parameter.")]
    public string ReturnUrl { get; set; }
}

public class CheckoutResultModel
{
    public string SessionId { get; set; }
    public string ClientSecret { get; set; }
}