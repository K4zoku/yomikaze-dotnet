using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Yomikaze.Application.Data.Models.Request;
using Yomikaze.Application.Data.Models.Response;
using Yomikaze.Domain.Constants;
using Yomikaze.WebAPI.Helpers;
using Yomikaze.WebAPI.Services;

namespace Yomikaze.WebAPI.Controllers;

[ApiController]
[Route($"API/{Api.Version}/[controller]")]
public class AuthenticationController : ControllerBase
{
    private AuthenticationService AuthenticationService { get; }

    public AuthenticationController(AuthenticationService authenticationService)
    {
        AuthenticationService = authenticationService;
    }

    [HttpPost]
    [Route(nameof(SignIn))]
    public async Task<ActionResult<ResponseModel<TokenModel>>> SignIn([FromBody] SignInModel model)
    {
        try
        {
            return Ok(ResponseModel.CreateSuccess("Sign-in successfully!", await AuthenticationService.SignIn(model)));
        }
        catch (ApiServiceException e)
        {
            return BadRequest(ResponseModel.CreateError(e.Message));
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, ResponseModel.CreateError(e.Message));
        }
    }

    [HttpPost]
    [Route(nameof(SignUp))]
    public async Task<ActionResult<ResponseModel<TokenModel>>> SignUp([FromBody] SignUpModel model)
    {
        try
        {
            return Ok(ResponseModel.CreateSuccess("Sign-up successfully!", await AuthenticationService.SignUp(model)));
        }
        catch (ApiServiceException e)
        {
            return BadRequest(ResponseModel.CreateError(e.Message));
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, ResponseModel.CreateError(e.Message));
        }
    }

    [HttpGet]
    [Authorize]
    public ActionResult<ResponseModel> Index() => Ok(ResponseModel.CreateSuccess("Authorized",
            new
            {
                Id = User.GetId(),
                Claims = User.Claims.ToDictionary(c => c.Type, c => c.Value)
            }
        ));
}