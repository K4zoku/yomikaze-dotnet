using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;
using Yomikaze.Application.Helpers.API;

namespace Yomikaze.API.Main.Controllers;

[ApiController]
[Route("profile")]
public class ProfileController(UserManager<User> userManager, IMapper mapper) : ControllerBase
{
    
    private UserManager<User> UserManager { get; } = userManager;
    
    private IMapper Mapper { get; } = mapper;
    
    [HttpGet]
    [Authorize]
    [SwaggerOperation(Summary = "Get the current user's profile.")]
    public async Task<ActionResult<ProfileModel>> GetCurrentUser()
    {
        var uid = User.GetId();
        return await GetUser(uid);
    }
    
    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get a user's profile by ID.")]
    public async Task<ActionResult<ProfileModel>> GetUser(ulong id)
    {
        User? user = await UserManager.FindByIdAsync(id.ToString());
        if (user is null)
        {
            return NotFound();
        }
        var model = Mapper.Map<ProfileModel>(user);
        if (User.TryGetId(out ulong currentId) && currentId == id)
        {
            model.Balance = user.Balance;
        }
        return model;
    }
    
    [HttpPatch]
    [Authorize]
    [SwaggerOperation(Summary = "Edit the current user's profile by patching it.")]
    public async Task<ActionResult> PatchCurrentUser(JsonPatchDocument<ProfileUpdateModel> patch)
    {
        var user = User.GetUser(UserManager);
        ProfileUpdateModel model = new();
        patch.ApplyTo(model);
        Mapper.Map(model, user);
        var result = await UserManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        return NoContent();
    }
    
    [HttpPut]
    [Authorize]
    [SwaggerOperation(Summary = "Change the current user's password.")] 
    public async Task<ActionResult<ProfileModel>> ChangePassword(ChangePasswordModel model)
    {
        var user = User.GetUser(UserManager);
        IdentityResult result;
        if (user.PasswordHash is null)
        {
            result = await UserManager.AddPasswordAsync(user, model.NewPassword);
        } 
        else
        {
            if (model.CurrentPassword is null)
            {
                ModelState.AddModelError(nameof(model.CurrentPassword), "Current password is required.");
                return ValidationProblem(ModelState);
            }
            result = await UserManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        }
        if (result.Succeeded)
        {
            return NoContent();
        } 
        return BadRequest(result.Errors);
    }   
    
}