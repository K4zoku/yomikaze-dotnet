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
public class LibraryController(DbContext dbContext) : ODataController
{
    private LibraryRepo Repository { get; set; } = new LibraryRepo(dbContext);

    [Authorize]

    public ActionResult<IEnumerable<LibraryEntry>> Get()
    {   
        long id = User.GetId();
        return Ok(Repository.GetLibraryByUserId(id));
    }
}