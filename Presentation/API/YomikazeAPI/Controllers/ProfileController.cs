using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
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
    public async Task<ActionResult<ProfileModel>> PatchCurrentUser(JsonPatchDocument<BaseModel> patch)
    {
        var uid = User.GetId();
        // TODO)) Implement patching the user profile, create model for the profile patch
        return NoContent();
    }
    
    [HttpPut]
    [Authorize]
    [SwaggerOperation(Summary = "Change the current user's password.")] 
    public async Task<ActionResult<ProfileModel>> ChangePassword(BaseModel model)
    {
        var uid = User.GetId();
        // TODO)) Implement changing the user password, create model for the password change
        return NoContent();
    }
    
}