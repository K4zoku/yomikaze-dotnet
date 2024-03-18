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
public class LibraryController(DbContext dbContext) : ControllerBase
{
    private LibraryRepo Repository { get; } = new(dbContext);


    public ActionResult<IEnumerable<LibraryEntry>> Get()
    {
        ulong id = User.GetId();
        return Ok(Repository.GetLibraryByUserId(id));
    }
}