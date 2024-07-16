using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using Stripe;
using Stripe.Checkout;
using Swashbuckle.AspNetCore.Annotations;
using System.Web;
using Yomikaze.API.Main.Configurations;
using Yomikaze.API.Main.Helpers;
using Yomikaze.Application.Helpers.API;

namespace Yomikaze.API.Main.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CoinPricingController(
    CoinPricingRepository repository,
    IMapper mapper,
    IDistributedCache cache,
    SessionService sessionService,
    PriceService priceService,
    StripeConfig stripeConfig,
    ILogger<CoinPricingController> logger)
    : CrudControllerBase<CoinPricing, CoinPricingModel, CoinPricingRepository>(repository, mapper, cache, logger)
{
    
    private SessionService SessionService { get; } = sessionService;
    private PriceService PriceService { get; } = priceService;
    
    private StripeConfig StripeConfig { get; } = stripeConfig;
    
    [AllowAnonymous]
    [HttpGet]
    public override ActionResult<PagedList<CoinPricingModel>> List(PaginationModel pagination)
    {
        return base.List(pagination);
    }
    
    [HttpPost]
    [Authorize(Roles = "Super,Administrator")]
    public override ActionResult<CoinPricingModel> Post(CoinPricingModel input)
    {
        var options = new PriceCreateOptions
        {
            Currency = input.Currency.ToString().ToLower(),
            UnitAmount = input.Amount,
            ProductData = new PriceProductDataOptions { Name = $"{input.Amount} Yomikaze Coin" },
        };
        var result = PriceService.Create(options);
        input.StripePriceId = result.Id;
        return base.Post(input);
    }
    
    [HttpPatch("{key}")]
    [Authorize(Roles = "Super,Administrator")]
    public override ActionResult<CoinPricingModel> Patch(ulong key, JsonPatchDocument<CoinPricingModel> patch)
    {if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }
        CoinPricing? entityToUpdate = Repository.Get(key);
        if (entityToUpdate == null)
        {
            return NotFound();
        }
        
        CoinPricingModel model = Mapper.Map<CoinPricingModel>(entityToUpdate);
        Logger.LogDebug("Patching model: {Model}", JsonConvert.SerializeObject(model));
        patch.ApplyTo(model);
        Logger.LogDebug("Patched model: {Model}", JsonConvert.SerializeObject(model));
        var options = new PriceCreateOptions
        {
            Currency = model.Currency.ToString().ToLower(),
            UnitAmount = model.Amount,
            ProductData = new PriceProductDataOptions { Name = $"{model.Amount} Yomikaze Coin" },
        };
        var result = PriceService.Create(options);
        model.StripePriceId = result.Id;
        Mapper.Map(model, entityToUpdate);
        try
        {
            Repository.Update(entityToUpdate);
        }
        catch (DbUpdateException e)
        {
            Logger.LogWarning(e, "DbRelation error when updating entity {Key}", key);
            return Conflict();
        } catch (Exception e)
        {
            Logger.LogCritical(e, "Critical error when updating entity {Key}", key);
            return Problem();
        }
        RemoveCache(key);
        return NoContent();
    }
    
    
    
    [HttpPost("/checkout")]
    [SwaggerOperation(Summary = "Create a new checkout session")]
    public ActionResult<CheckoutResultModel> Checkout([FromBody] CheckoutModel model)
    {
        if (!ulong.TryParse(model.PriceId, out ulong priceId))
        {
            ModelState.AddModelError(nameof(model.PriceId), "Invalid price ID.");
            return BadRequest(ModelState);
        }
        
        UriBuilder url;
        try
        {
            url = new UriBuilder(model.ReturnUrl);
        } 
        catch (UriFormatException)
        {
            ModelState.AddModelError(nameof(model.ReturnUrl), "Invalid return URL.");
            return BadRequest(ModelState);
        }
        var query = HttpUtility.ParseQueryString(url.Query);
        query.Set("session_id", "CHECKOUT_SESSION_ID");
        url.Query = query.ToString();
        
        CoinPricing? pricing = Repository.Get(priceId);
        if (pricing == null)
        {
            return NotFound();
        }
        var options = new SessionCreateOptions
        {
            UiMode = "embedded",
            LineItems =
            {
                new SessionLineItemOptions
                {
                    DynamicTaxRates = null,
                    Price = pricing.StripePriceId
                }
            },
            ExpiresAt = DateTime.Now.AddMinutes(15),
            Mode = "payment",
            ClientReferenceId = User.GetIdString(),
            ReturnUrl = url.ToString().Replace("CHECKOUT_SESSION_ID", "{CHECKOUT_SESSION_ID}") // Replace placeholder with actual placeholder (without url encoded)
        };
        Session session = SessionService.Create(options);
        return CreatedAtAction("CheckoutStatus", new { sessionId = session.Id }, new CheckoutResultModel { SessionId = session.Id, ClientSecret = session.ClientSecret });
    }
    
    private bool TryGetSession(string sessionId, out Session session)
    {
        try 
        {
            session = SessionService.Get(sessionId);
            return true;
        }
        catch (Exception e)
        {
            session = default!;
            Logger.LogDebug(e, "Failed to get session {SessionId}", sessionId);   
            return false;
        }
    }
    
    [HttpGet("/checkout/{sessionId}")]
    [SwaggerOperation(Summary = "Get the status of a checkout session")]
    public ActionResult CheckoutStatus(string sessionId)
    {
        if (!TryGetSession(sessionId, out var session))
        {
            return NotFound();
        }
        return Ok(session.RawJObject);
    }
    
    [HttpPut("/checkout/{sessionId}")] 
    [SwaggerOperation(Summary = "Client MUST call this endpoint right after checkout is completed and redirected to the return URL.")]
    public async Task<ActionResult> CheckoutComplete(string sessionId, [FromServices] UserManager<User> userManager)
    {
        if (!TryGetSession(sessionId, out var session))
        {
            return NotFound();
        }
        string suid = session.ClientReferenceId;
        string uid = User.GetIdString();
        
        if (suid != uid)
        {
            return Forbid();
        }
        
        if (session.Status != "complete")
        {
            return BadRequest();
        }
        var coin = session.LineItems.First().Quantity;
        if (coin == null)
        {
            return Problem();
        }
        var user = User.GetUser(userManager);
        user.Balance += coin.Value;
        await userManager.UpdateAsync(user);
        // TODO)) Add transaction record
        return Ok();
    }
    
    [HttpDelete("/checkout/{sessionId}")]
    [SwaggerOperation(Summary = "Actively cancel a checkout session")]
    public ActionResult CheckoutCancel(string sessionId)
    {
        if (!TryGetSession(sessionId, out var session))
        {
            return NotFound();
        }
        if (session.Status != "open")
        {
            return BadRequest();
        }
        string suid = session.ClientReferenceId;
        string uid = User.GetIdString();
        if (suid != uid)
        {
            return Forbid();
        }
        SessionService.Expire(sessionId);
        return NoContent();
    }
    
    [HttpPost("/payment-sheet")]
    public ActionResult<object> CreatePaymentSheet([FromBody][Bind(nameof(model.PriceId))] CheckoutModel model, [FromServices] PaymentIntentService paymentIntentService)
    {
        if (!ulong.TryParse(model.PriceId, out ulong priceId))
        {
            ModelState.AddModelError(nameof(model.PriceId), "Invalid price ID.");
            return BadRequest(ModelState);
        }
        CoinPricing? pricing = Repository.Get(priceId);
        if (pricing == null)
        {
            return NotFound();
        }
        
        var paymentIntentOptions = new PaymentIntentCreateOptions
        {
            Amount = pricing.Amount,
            Currency = pricing.Currency.ToString(),
        };
        PaymentIntent paymentIntent = paymentIntentService.Create(paymentIntentOptions);
        
        var result = new
        {
            PaymentIntent = paymentIntent.ClientSecret,
            PublishableKey = StripeConfig.PublishableKey,
        };
        return Ok(result);
    }

    [HttpPost("/stripe/webhook")]
    public async Task<IActionResult> Webhook([FromHeader(Name = "Stripe-Signature")] string signature)
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        try
        {
            var stripeEvent = EventUtility.ConstructEvent(json,
                signature, StripeConfig.WebhookSecret);

            // Handle the event
            Logger.LogDebug("Received stripe event type: {Type}", stripeEvent.Type);
            Logger.LogDebug("Raw data: {Data}", json);

            return Ok();
        }
        catch (StripeException e)
        {
            return BadRequest();
        }
    }
}