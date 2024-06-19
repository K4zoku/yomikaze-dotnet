using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Yomikaze.Domain.Identity.Entities;

namespace Yomikaze.API.Authentication;

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
            context.Fail();
        }

        context.Succeed(new OperationAuthorizationRequirement());
    }
}