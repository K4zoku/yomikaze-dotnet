using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Security.Claims;

namespace Yomikaze.API.Main.Configurations;

public class SidValidationAuthorizationHandler(SignInManager<User> signInManager) : IAuthorizationHandler
{
    private SignInManager<User> SignInManager => signInManager;

    public async Task HandleAsync(AuthorizationHandlerContext context)
    {
        ClaimsPrincipal principal = context.User;
        if (!(principal.Identity?.IsAuthenticated ?? false))
        {
            return;
        }

        User? user = await SignInManager.ValidateSecurityStampAsync(principal);
        if (user is null)
        {
            if (context.Resource is HttpContext httpContext)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            context.Fail();
            return;
        }

        context.Succeed(new OperationAuthorizationRequirement());
    }
}