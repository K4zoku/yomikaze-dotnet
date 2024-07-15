using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using Stripe;
using Stripe.Checkout;
using System.Web;
using Yomikaze.API.Main.Helpers;
using Yomikaze.Application.Helpers.API;

namespace Yomikaze.API.Main.Controllers;

[Authorize(Roles = "Super,Administrator")]
[ApiController]
[Route("[controller]")]
public class CoinPricingController(
    CoinPricingRepository repository,
    IMapper mapper,
    IDistributedCache cache,
    ILogger<CoinPricingController> logger)
    : CrudControllerBase<CoinPricing, CoinPricingModel, CoinPricingRepository>(repository, mapper, cache, logger)
{
    [AllowAnonymous]
    [HttpGet]
    public override ActionResult<PagedList<CoinPricingModel>> List(PaginationModel pagination)
    {
        return base.List(pagination);
    }

    [NonAction]
    public override ActionResult<CoinPricingModel> Post(CoinPricingModel input)
    {
        return base.Post(input);
    }
    
    [HttpPost]
    public ActionResult<CoinPricingModel> Post(CoinPricingModel input, [FromServices] PriceService priceService)
    {
        var options = new PriceCreateOptions
        {
            Currency = input.Currency.ToString().ToLower(),
            UnitAmount = input.Amount,
            ProductData = new PriceProductDataOptions { Name = $"{input.Amount} Yomikaze Coin" },
        };
        var result = priceService.Create(options);
        input.StripePriceId = result.Id;
        return base.Post(input);
    }
    
    [NonAction]
    public override ActionResult<CoinPricingModel> Patch(ulong key, JsonPatchDocument<CoinPricingModel> patch)
    {
        return base.Patch(key, patch);
    }
    
    [HttpPatch("{key}")]
    public ActionResult<CoinPricingModel> Patch(ulong key, JsonPatchDocument<CoinPricingModel> patch, [FromServices] PriceService priceService)
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
        var result = priceService.Create(options);
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

    [Authorize]
    [HttpGet("{key}/[action]")]
    public ActionResult Checkout(ulong key, [FromQuery] string returnUrl, [FromServices] SessionService sessionService)
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
        
        CoinPricing? pricing = Repository.Get(key);
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
            ReturnUrl = url.ToString().Replace("CHECKOUT_SESSION_ID", "{CHECKOUT_SESSION_ID}") // Replace placeholder with actual placeholder (without url encoded)
        };
        Session session = sessionService.Create(options);
        Cache.GetOrSet(session.Id, () => User.GetIdString(), new DistributedCacheEntryOptions() { AbsoluteExpiration = session.ExpiresAt });
        return CreatedAtAction("CheckoutStatus", new { session_id = session.Id}, new { clientSecret = session.ClientSecret });
    }
    
    [HttpGet("/checkout/{sessionId}/status")]
    public ActionResult CheckoutStatus(string sessionId)
    {
        var sessionService = new SessionService();
        Session session = sessionService.Get(sessionId);

        return Ok(session.RawJObject);
    }
    
    [HttpGet("/checkout/{sessionId}/success")]
    public ActionResult CheckoutSuccess(string sessionId)
    {
        string? userId = Cache.GetAsString(sessionId);
        if (userId == null)
        {
            return NotFound();
        }
        Cache.Remove(sessionId);
        // TODO)) Check current authenticated user is the same as the user who initiated the session
        // TODO)) Add coins to user's balance
        
        return Ok();
    }
    
    [HttpGet("/checkout/{sessionId}/cancel")]
    public ActionResult CheckoutCancel(string sessionId)
    {
        return Ok();
    }
}