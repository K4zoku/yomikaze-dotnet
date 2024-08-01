using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Security.Claims;

namespace Yomikaze.API.Main.Configurations;

public class SidValidationAuthorizationHandler(SignInManager<User> signInManager) : IAuthorizationMiddlewareResultHandler
{
    private SignInManager<User> SignInManager => signInManager;
    
    private readonly AuthorizationMiddlewareResultHandler _defaultHandler = new();

    public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy,
        PolicyAuthorizationResult authorizeResult)
    {
        ClaimsPrincipal principal = context.User;
        if (!(principal.Identity?.IsAuthenticated ?? false))
        {
            return;
        }

        User? user = await SignInManager.ValidateSecurityStampAsync(principal);
        if (user is null)
        {
            
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return;
        }

        await _defaultHandler.HandleAsync(next, context, policy, authorizeResult);
    }
}