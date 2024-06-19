using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Yomikaze.Application.Data.Repos;
using Yomikaze.Application.Helpers.API;
using Yomikaze.Domain.Entities;
using Yomikaze.Domain.Entities.Weak;

namespace Yomikaze.API.OData.Controllers;

[EnableQuery]
[Authorize]
public class LibraryController(DbContext dbContext) : ControllerBase
{
    private LibraryRepository Repository { get; } = new(dbContext);


    public ActionResult<IEnumerable<LibraryEntry>> Get()
    {
        string id = User.GetIdString();
        return Ok(Repository.GetLibraryByUserId(id));
    }
}