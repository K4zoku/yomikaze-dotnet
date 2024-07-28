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
    TransactionRepository transactionRepository,
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
    
    private TransactionRepository TransactionRepository { get; } = transactionRepository;
    
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
            UnitAmount = Convert.ToInt64(input.Price) * 100,
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
            UnitAmount = Convert.ToInt64(model.Price) * 100,
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
    
    
    
    [HttpPost("/stripe/checkout")]
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
                    Price = pricing.StripePriceId,
                }
            },
            Metadata = new Dictionary<string, string>()
            {
                ["UserId"] = User.GetIdString(),
                ["PriceId"] = pricing.Id.ToString(),
            },
            ExpiresAt = DateTime.Now.AddMinutes(15),
            Mode = "payment",
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
    
    [HttpGet("/stripe/checkout/{sessionId}")]
    [SwaggerOperation(Summary = "Get the status of a checkout session")]
    public ActionResult<StripeResponse> CheckoutStatus(string sessionId)
    {
        if (!TryGetSession(sessionId, out var session))
        {
            return NotFound();
        }
        return Ok(session.RawJObject);
    }
    
    [HttpPost("/stripe/payment-sheet")]
    [Authorize]
    public ActionResult<PaymentSheetResultModel> CreatePaymentSheet([FromBody] PaymentSheetModel model, [FromServices] PaymentIntentService paymentIntentService, [FromServices] UserManager<User> userManager)
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

        var user = User.GetUser(userManager);
        var isChanged = false;
        if (user.StripeCustomerId == null)
        {
            var customerOptions = new CustomerCreateOptions() { Email = user.Email, Name = user.Name };
            var customerService = new CustomerService();
            var customer = customerService.Create(customerOptions);
            user.StripeCustomerId = customer.Id;
            isChanged = true;
        }

        if (user.StripeEphemeralKey == null)
        {
            var ephemeralKeyOptions = new EphemeralKeyCreateOptions
            {
                Customer = user.StripeCustomerId, StripeVersion = "2024-06-20",
            };
            var ephemeralKeyService = new EphemeralKeyService();
            var ephemeralKey = ephemeralKeyService.Create(ephemeralKeyOptions);
            user.StripeEphemeralKey = ephemeralKey.Secret;
            isChanged = true;
        }

        if (isChanged)
        {
            userManager.UpdateAsync(user).Wait();
        }

        var paymentIntentOptions = new PaymentIntentCreateOptions
        {
            Amount = Convert.ToInt64(pricing.Price) * 100,
            Currency = pricing.Currency.ToString(),
            Customer = user.StripeCustomerId,
            Metadata = new Dictionary<string, string>()
            {
                ["UserId"] = User.GetIdString(),
                ["PriceId"] = pricing.Id.ToString(),
            }
        };
        PaymentIntent paymentIntent = paymentIntentService.Create(paymentIntentOptions);
        
        var result = new PaymentSheetResultModel
        {
            ClientSecret = paymentIntent.ClientSecret,
            EphemeralKey = user.StripeEphemeralKey,
            Customer = user.StripeCustomerId,
            PublishableKey = StripeConfig.PublishableKey,
        };
        return Ok(result);
    }

    [HttpPost("/stripe/webhook")]
    [AllowAnonymous]    
    public async Task<IActionResult> Webhook([FromHeader(Name = "Stripe-Signature")] string signature, [FromServices] UserManager<User> userManager)
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        Event stripeEvent;
        try
        {
            stripeEvent = EventUtility.ConstructEvent(json,
                signature, StripeConfig.WebhookSecret);
        }
        catch (StripeException e)
        {
            Logger.LogWarning(e, "Stripe error when handling webhook");
            return BadRequest();
        }

        Dictionary<string, string> metadata;
        switch (stripeEvent.Type)
        {
            case Events.CheckoutSessionCompleted:
                if (stripeEvent.Data.Object is not Session session)
                {
                    Logger.LogWarning("Invalid session object in event data");
                    return BadRequest();
                }
                metadata = session.Metadata;
                break;
            case Events.PaymentIntentSucceeded:
                if (stripeEvent.Data.Object is not PaymentIntent paymentIntent)
                {
                    Logger.LogWarning("Invalid payment intent object in event data");
                    return BadRequest();
                }
                metadata = paymentIntent.Metadata;
                break;
            default:
                metadata = new Dictionary<string, string>();
                break;
        }
        
        if (!metadata.TryGetValue("PriceId", out string? priceIdStr))
        {
            ModelState.AddModelError("Metadata.PriceId", "Price ID not found in metadata");
            return BadRequest(ModelState);
        }
        if (!metadata.TryGetValue("UserId", out string? userId))
        {
            ModelState.AddModelError("Metadata.UserId", "User ID not found in metadata");
            return BadRequest(ModelState);
        }
        if (!ulong.TryParse(priceIdStr, out ulong priceId))
        {
            ModelState.AddModelError("Metadata.PriceId", "Invalid price ID");
            return BadRequest(ModelState);
        }
        
        Logger.LogDebug("Getting CoinPricing {PriceId}", priceId);
        CoinPricing? coin = Repository.Get(priceId);
        if (coin == null)
        {
            Logger.LogWarning("Coin pricing {Id} not found", priceId);
            return NotFound();
        }
        Logger.LogDebug("Getting User {UserId}", userId);
        User? user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            Logger.LogWarning("User {Id} not found", userId);
            return NotFound();
        }
            
        user.Balance += coin.Amount;
        await userManager.UpdateAsync(user);
        Transaction transaction = new() { Amount = coin.Amount, UserId = user.Id, Description = "Coin purchase", Type = TransactionType.PurchaseCoin };
        TransactionRepository.Add(transaction);
        return Ok();
    }
}