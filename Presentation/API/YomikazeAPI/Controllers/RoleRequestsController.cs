using Microsoft.AspNetCore.Identity;
using Yomikaze.Application.Helpers.API;
using Yomikaze.Domain.Models.Search;

namespace Yomikaze.API.Main.Controllers;

[ApiController]
[Route("/roles/requests")]
public class RoleRequestsController(
    RoleRequestRepository repository,
    RoleManager<Role> roleManager,
    IMapper mapper,
    ILogger<RoleRequestsController> logger)
    : SearchControllerBase<RoleRequest, RoleRequestModel, RoleRequestRepository, RoleRequestSearchModel>(repository,
        mapper, logger)
{
    private RoleManager<Role> RoleManager { get; } = roleManager;

    protected override IList<SearchFieldMutator<RoleRequest, RoleRequestSearchModel>> SearchFieldMutators { get; } =
    [
        new SearchFieldMutator<RoleRequest, RoleRequestSearchModel>(
            search => search.Status.HasValue,
            (query, search) => query.Where(x => x.Status == search.Status)
        ),
        new SearchFieldMutator<RoleRequest, RoleRequestSearchModel>(
            search => search.OrderBy == RoleRequestOrderBy.CreationTime,
            (query, search) => query.OrderBy(x => x.CreationTime)
        ),
        new SearchFieldMutator<RoleRequest, RoleRequestSearchModel>(
            search => search.OrderBy == RoleRequestOrderBy.CreationTimeDesc,
            (query, search) => query.OrderByDescending(x => x.CreationTime)
        )
    ];

    [Authorize(Roles = "Reader")]
    public override ActionResult<RoleRequestModel> Post(RoleRequestModel input)
    {
        string userId = User.GetIdString();
        Role? role = RoleManager.FindByNameAsync("Publisher").Result;
        if (role == null)
        {
            return NotFound("Role not found");
        }

        input.UserId = userId;
        input.RoleId = role.Id.ToString();
        return base.Post(input);
    }

    [HttpPut]
    [Route("{id}/approve")]
    [Authorize(Roles = "Super,Administrator")]
    public async Task<ActionResult<RoleRequestModel>> Approve(ulong id, UserManager<User> userManager)
    {
        RoleRequest? roleRequest = Repository.Get(id);
        if (roleRequest == null)
        {
            return NotFound("Role request not found");
        }

        roleRequest.Status = RoleRequestStatus.Approved;
        Repository.Update(roleRequest);
        User? user = await userManager.FindByIdAsync(roleRequest.UserId.ToString());
        await userManager.AddToRoleAsync(user!, roleRequest.Role.Name!);
        return Mapper.Map<RoleRequestModel>(roleRequest);
    }

    [HttpPut]
    [Route("{id}/reject")]
    [Authorize(Roles = "Super,Administrator")]
    public ActionResult<RoleRequestModel> Reject(ulong id)
    {
        RoleRequest? roleRequest = Repository.Get(id);
        if (roleRequest == null)
        {
            return NotFound("Role request not found");
        }

        roleRequest.Status = RoleRequestStatus.Rejected;
        Repository.Update(roleRequest);
        return Mapper.Map<RoleRequestModel>(roleRequest);
    }
}