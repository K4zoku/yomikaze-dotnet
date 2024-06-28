using Stripe.Checkout;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Yomikaze.API.Main.Controllers;

[ApiController]
[Route("[controller]")]
public class CheckoutController : ControllerBase
{
    [HttpPost]
    public ActionResult CreateCheckoutSession([Range(100, 10000)] int coins, string returnUrl)
    {
        UriBuilder url;
        try
        {
            url = new UriBuilder(returnUrl);
        } 
        catch (UriFormatException)
        {
            ModelState.AddModelError(nameof(returnUrl), "Invalid return URL.");
            return BadRequest(ModelState);
        }

        var query = HttpUtility.ParseQueryString(url.Query);
        query.Set("session_id", "CHECKOUT_SESSION_ID");
        url.Query = query.ToString();
        
        var options = new SessionCreateOptions()
        {
            UiMode = "embedded",
            LineItems =
            [
                new SessionLineItemOptions
                {
                    DynamicTaxRates = null,
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        ProductData =
                            new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Yomikaze Coin",
                            },
                        UnitAmount = 12,    // 1 coin = $0.12           
                    },
                    Quantity = coins,
                }
            ],
            Mode = "payment",
            ReturnUrl = url.ToString().Replace("CHECKOUT_SESSION_ID", "{CHECKOUT_SESSION_ID}") // Replace placeholder with actual placeholder (without url encoded)
        };
        var service = new SessionService();
        Session session = service.Create(options);
        return CreatedAtAction("SessionStatus", new { session_id = session.Id}, new { clientSecret = session.ClientSecret });
    }
    
    [HttpGet("status")]
    public ActionResult SessionStatus([FromQuery(Name = "session_id")] string sessionId)
    {
        var sessionService = new SessionService();
        Session session = sessionService.Get(sessionId);

        return Ok(session.RawJObject);
    }
}