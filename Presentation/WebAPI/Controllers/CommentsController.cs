using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Yomikaze.Domain.Constants;
using Yomikaze.Domain.Database.Entities;
using Yomikaze.WebAPI.Models.Common;
using Yomikaze.WebAPI.Models.Request;
using Yomikaze.WebAPI.Models.Response;
using Yomikaze.WebAPI.Services;

namespace Yomikaze.WebAPI.Controllers;

[Route($"API/{Api.Version}/[controller]")]
[ApiController]
public class CommentsController : ControllerBase
{
    //service
    private readonly CommentService _commentService;

    public CommentsController(CommentService commentService)
    {
        _commentService = commentService;
    }

    // create comment
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ResponseModel<CommentModel>>> CreateCommentAsync([FromBody] CommentRequestModel comment)
    {
        try
        {
            var model = await _commentService.CreateCommentAsync(comment, User);
            return Ok(ResponseModel.CreateSuccess("Comment successfully", model));
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

    // get comment
    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseModel<CommentModel>>> GetCommentAsync(long id)
    {
        try
        {
            var comment = await _commentService.GetCommentAsync(id);
            return Ok(ResponseModel.CreateSuccess("Get comment successfully", comment));
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

    // update comment
    [HttpPatch("{id}")]
    [Authorize]
    public async Task<ActionResult<ResponseModel<CommentModel>>> UpdateCommentAsync([FromRoute] long id, [FromBody] string content)
    {
        try
        {
            var model = new CommentRequestModel { Id = id, Content = content };
            var comment = await _commentService.UpdateCommentAsync(model, User);
            return Ok(ResponseModel.CreateSuccess("Update comment successfully", comment));
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

    // delete comment
    [HttpDelete("{id}")]
    public async Task<ActionResult<ResponseModel>> DeleteCommentAsync(long id)
    {
        try
        {
            var model = new CommentRequestModel { Id = id, Content = "" };
            await _commentService.DeleteCommentAsync(model);
            return Ok(ResponseModel.CreateSuccess("Delete comment successfully"));
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

    // get all comments
    [HttpGet]
    [Route($"/API/{Api.Version}/Comics/{{cid}}/[controller]")]
    public async Task<ActionResult<ResponseModel<IEnumerable<Comment>>>> GetAllCommentsAsync([FromRoute(Name = "cid")] long comicId)
    {
        try
        {
            var comments = await _commentService.GetCommentsAsync(comicId);
            return Ok(ResponseModel.CreateSuccess("Get all comments successfully", comments));
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
