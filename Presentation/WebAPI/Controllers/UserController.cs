using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Yomikaze.Application.Data.Models;
using Yomikaze.Application.Data.Models.Common;
using Yomikaze.Application.Data.Models.Response;
using Yomikaze.Domain.Constants;
using Yomikaze.Domain.Database.Entities.Identity;

namespace Yomikaze.WebAPI.Controllers;

[ApiController]
[Route($"API/{Api.Version}/[controller]")]
public class UserController : ControllerBase
{
    private UserManager<User> UserManager { get; }

    public UserController(UserManager<User> userManager)
    {
        UserManager = userManager;
    }

    [HttpGet]
    [Route("Info/{uid}")]
    public async Task<ActionResult<ResponseModel<UserModel>>> GetUserInfo(long uid)
    {
        var user = await UserManager.FindByIdAsync(uid.ToString());
        if (user is null) return NotFound(ResponseModel.CreateError("User not found!"));
        return Ok(ResponseModel.CreateSuccess(user.ToModel()));
    }

}