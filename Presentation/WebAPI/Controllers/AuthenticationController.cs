using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using Yomikaze.Domain.Constants;
using Yomikaze.Domain.Database.Entities.Identity;
using Yomikaze.WebAPI.Helpers;
using Yomikaze.WebAPI.Models.Request;
using Yomikaze.WebAPI.Models.Response;

namespace Yomikaze.WebAPI.Controllers;

[ApiController]
[Route($"API/{Api.Version}/[controller]")]
public class AuthenticationController : ControllerBase
{
    private UserManager<User> UserManager { get; }

    private JwtConfiguration Jwt { get; }

    public AuthenticationController(UserManager<User> userManager, JwtConfiguration jwtConfiguration)
    {
        UserManager = userManager;
        Jwt = jwtConfiguration;
    }

    [HttpPost]
    [Route(nameof(SignIn))]
    public async Task<ActionResult<ResponseModel<TokenModel>>> SignIn([FromBody] SignInModel model)
    {
        var user = await UserManager.FindByNameAsync(model.Username);
        if (user is null) return Unauthorized(ResponseModel.CreateError("User not found!"));

        var passwordMatched = await UserManager.CheckPasswordAsync(user, model.Password);
        if (!passwordMatched) return Unauthorized(ResponseModel.CreateError("Password does not match"));

        var token = GetToken(user).ToTokenString();
        return Ok(ResponseModel.CreateSuccess("Sign in successful", new TokenModel(token)));
    }

    [HttpPost]
    [Route(nameof(SignUp))]
    public async Task<ActionResult<ResponseModel<TokenModel>>> SignUp([FromBody] SignUpModel model)
    {
        var user = await UserManager.FindByNameAsync(model.Username) ?? await UserManager.FindByEmailAsync(model.Email);
        if (user is not null) return BadRequest(ResponseModel.CreateError("Username or email already exists"));

        user = new User
        {
            UserName = model.Username,
            Email = model.Email,
            Birthday = model.Birthday
        };

        var result = await UserManager.CreateAsync(user, model.Password);
        if (result.Succeeded) return Ok(ResponseModel.CreateSuccess("Sign up successful", new TokenModel(GetToken(user))));

        return StatusCode((int)HttpStatusCode.InternalServerError,
            ResponseModel.CreateError(
                "There was error while creating account",
                result.Errors.Select(e => e.Description)
            )
        );

    }

    [HttpGet]
    [Authorize]
    public ActionResult<ResponseModel> Index()
    {
        return Ok(ResponseModel.CreateSuccess("Authorized",
            new
            {
                Id = User.GetId(),
                Claims = User.Claims.ToDictionary(c => c.Type, c => c.Value)
            }
        ));
    }

    [NonAction]
    private JwtSecurityToken GetToken(IEnumerable<Claim> claims)
    {
        var token = new JwtSecurityToken(
            issuer: Jwt.Issuer,
            audience: Jwt.Audience,
            signingCredentials: Jwt.SigningCredentials,
            claims: claims
        );
        return token;
    }

    [NonAction]
    private JwtSecurityToken GetToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
            new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
        };
        return GetToken(claims);
    }

}