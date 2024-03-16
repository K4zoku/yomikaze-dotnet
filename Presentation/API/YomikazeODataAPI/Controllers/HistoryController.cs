using Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using Yomikaze.API.OData.Base;
using Yomikaze.Application.Data.Repos;
using Yomikaze.Application.Helpers.API;
using Yomikaze.Domain.Entities;

namespace Yomikaze.API.OData.Controllers;
[EnableQuery]
[Authorize]
public class HistoryController(DbContext dbContext) : ControllerBase
{
    private HistoryRepo Repository { get; set; } = new HistoryRepo(dbContext);

    // example of a get request
    // Chapter($expand= Comic)
    public ActionResult<IEnumerable<LibraryEntry>> Get()
    {   
        long id = User.GetId();
        return Ok(Repository.GetHistoryByUserId(id));
    }
}