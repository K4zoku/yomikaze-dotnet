using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Yomikaze.Domain.Common;
using Yomikaze.Domain.Constants;
using Yomikaze.Domain.Database.Entities;
using Yomikaze.Domain.Database.Entities.Identity;
using Yomikaze.WebAPI.Models;
using Yomikaze.WebAPI.Services;

namespace Yomikaze.WebAPI.Controllers;

[Route($"API/{Api.Version}/[controller]")]
[ApiController]
public class CommentsController : ControllerBase
{
    //service
    private readonly CommentService _commentService;
    private readonly IDao<Comic> _comicDao;
    private readonly UserManager<YomikazeUser> _userManager;

    public CommentsController(CommentService commentService, IDao<Comic> comicDao, UserManager<YomikazeUser> userManager)
    {
        _commentService = commentService;
        _comicDao = comicDao;
        _userManager = userManager;
    }

    // create comment
    [HttpPost]
    public async Task<ActionResult> CreateCommentAsync([FromBody] CommentRequest comment)
    {
        var comic = await _comicDao.GetAsync(comment.ComicId);
        if (comic == null)
        {
            return NotFound();
        }
        var yomikazeUser = await _userManager.GetUserAsync(User);
        if (yomikazeUser == null)
        {
            return NotFound();
        }
        var newComment = await _commentService.CreateCommentAsync(comic, yomikazeUser, comment.Content);
        return Ok(new Response { Success = true, Message = "Comment successfully", Data = newComment });
    }

    // get comment
    [HttpGet("{id}")]
    public async Task<ActionResult<Yomikaze.Domain.Database.Entities.Comment>> GetCommentAsync(long id)
    {
        var comment = await _commentService.GetCommentAsync(id);
        if (comment == null)
        {
            return NotFound();
        }
        return comment;
    }

    // update comment
    [HttpPatch("{id}")]
    public async Task<ActionResult> UpdateCommentAsync([FromRoute] long id, [FromBody] string content)
    {
        var commentToUpdate = await _commentService.GetCommentAsync(id);
        if (commentToUpdate == null)
        {
            return NotFound();
        }
        var yomikazeUser = await _userManager.GetUserAsync(User);
        if (yomikazeUser == null)
        {
            return NotFound();
        }
        if (commentToUpdate.User != yomikazeUser)
        {
            return Unauthorized();
        }
        if (content == string.Empty)
        {
            // delete
            var deletedComment = await _commentService.DeleteCommentAsync(commentToUpdate);
            return Ok(new Response { Success = true, Message = "Delete comment successfully", Data = deletedComment });
        }
        var updatedComment = await _commentService.UpdateCommentAsync(commentToUpdate, content);
        return Ok(new Response { Success = true, Message = "Edit comment successfully", Data = updatedComment });
    }

    // delete comment
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCommentAsync(long id)
    {
        var commentToDelete = await _commentService.GetCommentAsync(id);
        if (commentToDelete == null)
        {
            return NotFound();
        }
        var yomikazeUser = await _userManager.GetUserAsync(User);
        if (yomikazeUser == null)
        {
            return NotFound();
        }
        if (commentToDelete.User != yomikazeUser)
        {
            return Unauthorized();
        }
        var deletedComment = await _commentService.DeleteCommentAsync(commentToDelete);
        return Ok(new Response { Success = true, Message = "Comment successfully", Data = deletedComment });
    }

    // get all comments
    [HttpGet]
    [Route($"/API/{Api.Version}/Comics/{{cid}}/[controller]")]
    public async Task<ActionResult<IEnumerable<Yomikaze.Domain.Database.Entities.Comment>>> GetAllCommentsAsync([FromRoute] long cid)
    {
        var comic = await _comicDao.GetAsync(cid);
        if (comic == null)
        {
            return NotFound(new Response
            {
                Success = false,
                Message = "Comic not found"
            });
        }
        var comments = await _commentService.GetCommentsAsync(comic);
        return Ok(new Response
        {
            Success = true,
            Message = "Get all comments successfully",
            Data = comments
        });
    }

}
