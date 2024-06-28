using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
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
            context.Fail(new AuthorizationFailureReason(this, "Invalid security stamp"));
        }

        context.Succeed(new OperationAuthorizationRequirement());
    }
}