using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Yomikaze.Application.Data.Repos;
using Yomikaze.Application.Helpers.API;
using Yomikaze.Domain.Entities;

namespace Yomikaze.API.OData.Controllers;

[EnableQuery]
[Authorize]
public class HistoryController(DbContext dbContext) : ControllerBase
{
    private HistoryRepo Repository { get; } = new(dbContext);

    // example of a get request
    // Chapter($expand= Comic)
    public ActionResult<IEnumerable<LibraryEntry>> Get()
    {
        string id = User.GetId();
        return Ok(Repository.GetHistoryByUserId(id));
    }
}