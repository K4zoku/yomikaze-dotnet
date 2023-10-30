using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Yomikaze.Domain.Common;
using Yomikaze.Domain.Database.Entities;
using Yomikaze.Domain.Database.Entities.Identity;
using Yomikaze.WebAPI.Models;
using Yomikaze.WebAPI.Services;

namespace Yomikaze.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    //service
    private readonly CommentService _commentService;
    private readonly IDao<Comic> _comicDao;
    private readonly UserManager<YomikazeUser> _userManager;

    public CommentController(CommentService commentService, IDao<Comic> comicDao, UserManager<YomikazeUser> userManager)
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
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCommentAsync(long id, [FromBody] CommentRequest comment)
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
        var updatedComment = await _commentService.UpdateCommentAsync(commentToUpdate, comment.Content);
        return Ok(new Response { Success = true, Message = "Comment successfully", Data = updatedComment });
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

}
