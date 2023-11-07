using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Yomikaze.Application.Data.Models.Common;
using Yomikaze.Application.Data.Models.Response;
using Yomikaze.Domain.Constants;
using Yomikaze.WebAPI.Services;

namespace Yomikaze.WebAPI.Controllers;

[Route($"API/{Api.Version}/[controller]")]
[ApiController]
[Authorize]
public class LibraryController : ControllerBase
{
    private readonly LibraryService _libraryService;

    public LibraryController(LibraryService libraryService)
    {
        _libraryService = libraryService;
    }

    [HttpGet]
    public async Task<ActionResult<ResponseModel<IEnumerable<LibraryEntryModel>>>> GetLibrary()
    {
        try
        {
            var library = await _libraryService.GetLibrary(User);
            return Ok(ResponseModel.CreateSuccess("Get library successfully", library));
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
    [Route("Add/{comicId}")]
    public async Task<ActionResult<ResponseModel<LibraryEntryModel>>> AddToLibrary(long comicId)
    {
        try
        {
            await _libraryService.AddToLibrary(comicId, User);
            return Ok(ResponseModel.CreateSuccess("Add to library successfully"));
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

    [HttpDelete]
    [Route("Remove/{comicId}")]
    public async Task<ActionResult<ResponseModel>> RemoveFromLibrary(long comicId)
    {
        try
        {
            await _libraryService.RemoveFromLibrary(comicId, User);
            return Ok(ResponseModel.CreateSuccess("Remove from library successfully"));
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
    [Route("IsInLibrary/{comicId}")]
    public async Task<ActionResult<ResponseModel<bool>>> IsInLibrary(long comicId)
    {
        try
        {
            var result = await _libraryService.IsInLibrary(comicId, User);
            return Ok(ResponseModel.CreateSuccess(result));
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
}
