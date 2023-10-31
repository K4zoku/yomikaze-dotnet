﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Yomikaze.Domain.Constants;
using Yomikaze.Domain.Database.Entities.Identity;
using Yomikaze.WebAPI.Helpers;
using Yomikaze.WebAPI.Models;

namespace Yomikaze.WebAPI.Controllers;

[ApiController]
[Route($"API/{Api.Version}/[controller]")]
public class AuthenticationController : ControllerBase
{
    private UserManager<YomikazeUser> UserManager { get; }

    private JwtConfiguration JwtConfiguration { get; }

    public AuthenticationController(UserManager<YomikazeUser> userManager, JwtConfiguration jwtConfiguration)
    {
        UserManager = userManager;
        JwtConfiguration = jwtConfiguration;
    }

    [HttpPost]
    [Route("SignIn")]
    public async Task<ActionResult<SignInResponse>> SignIn([FromBody] SignInRequest model)
    {
        var user = await UserManager.FindByNameAsync(model.Username);
        if (user == null)
        {
            return Unauthorized(new SignInResponse
            {
                Success = false,
                Message = "User not found"
            });
        }
        var result = await UserManager.CheckPasswordAsync(user, model.Password);
        if (!result)
        {
            return Unauthorized(new SignInResponse
            {
                Success = false,
                Message = "Password not match"
            });
        }

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
            new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
        };
        var token = GetToken(claims).ToTokenString();
        return Ok(new SignInResponse
        {
            Token = token,
            Success = true,
            Message = "Authentication successful"
        });
    }

    [HttpPost]
    [Route("SignUp")]
    public async Task<ActionResult<SignInResponse>> SignUp([FromBody] SignUpRequest model)
    {
        var user = await UserManager.FindByNameAsync(model.Username) ?? await UserManager.FindByEmailAsync(model.Email);
        if (user != null)
        {
            return BadRequest(new SignInResponse
            {
                Success = false,
                Message = "Account is already exist"
            });
        }

        if (!model.Password.Equals(model.ConfirmPassword))
        {
            return BadRequest(new SignInResponse
            {
                Success = false,
                Message = "Password and confirm password not match"
            });
        }

        user = new YomikazeUser() { UserName = model.Username, Email = model.Email, Birthday = model.Birthday };

        var result = await UserManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            // return 500
            return StatusCode(500, new SignInResponse
            {
                Success = false,
                Message = "There was error while creating account",
                Data = result.Errors.Select(e => e.Description)
            });
        }

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        };
        var token = GetToken(claims).ToTokenString();
        return Ok(new SignInResponse
        {
            Token = token,
            Success = true,
            Message = "Create account successful"
        });

    }

    [HttpGet]
    [Authorize]
    public ActionResult<Response> Get()
    {
        return Ok(value: new Response
        {
            Success = true,
            Message = "Authorized",
            Data = new
            {
                Id = User.GetId(),
                Claims = User.Claims.Select(c => new Dictionary<string, string> { { c.Type, c.Value } })
            }
        });
    }

    [NonAction]
    private JwtSecurityToken GetToken(IEnumerable<Claim> claims)
    {
        var token = new JwtSecurityToken(
            issuer: JwtConfiguration.Issuer,
            audience: JwtConfiguration.Audience,
            signingCredentials: JwtConfiguration.SigningCredentials,
            claims: claims
        );
        return token;
    }
}